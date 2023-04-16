
using Jwt_Login_API.Models;
using JWT_Login_Authentication.Configurations;
using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jwt_Login_API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly AppDbContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationController(UserManager<IdentityUser> userManager, IOptions<JwtConfig> jwtConfig,
            AppDbContext context, TokenValidationParameters tokenValidationParameters, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
            _context = context;
            _tokenValidationParameters = tokenValidationParameters;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route(template: "Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            // Validate the incomin request 
            if (ModelState.IsValid)
            {
                // We need to check if the email alreadly exists
                var user_exists = await _userManager.FindByEmailAsync(requestDto.Email);

                if (user_exists != null)
                {
                    return BadRequest(error: new AuthResult()
                    {
                        Result = false,
                        Error = new List<string>()
                        {
                            "Email alrealy exists"
                        }
                    });
                }

                // Create new user
                var newUser = new IdentityUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Name
                };

                var isCreated = await _userManager.CreateAsync(newUser, requestDto.Password);

                if (isCreated.Succeeded)
                {
                    // We need to add the user to a role
                    await _userManager.AddToRoleAsync(newUser, "AppUser");

                    // generate a Token
                    var token = await GenerateJwtToken(newUser);
                    return Ok(token);
                }

                return BadRequest(new AuthResult()
                {
                    Error = new List<string>()
                    {
                        "Server Error"
                    }
                });
            }
            return BadRequest(new AuthResult()
            {
                Error = new List<string>()
                    {
                        "Data InValid "
                    }
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto requestDto)
        {
            if (ModelState.IsValid)
            {
                // Check if user exists
                var existsing_user = await _userManager.FindByEmailAsync(requestDto.Email);
                if (existsing_user == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Error = new List<string>()
                        {
                            "Payload Invalid"
                        },
                        Result = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existsing_user, requestDto.Password);

                if (!isCorrect)
                {
                    return BadRequest(new AuthResult()
                    {
                        Error = new List<string>()
                        {
                            "Invalid Credential"
                        },
                        Result = false
                    });
                }

                var jwtToken = await GenerateJwtToken(existsing_user);

                return Ok(jwtToken);
            }
            return BadRequest(new AuthResult()
            {
                Error = new List<string>()
                {
                    "Payload Invalid"
                },
                Result = false
            });
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if(ModelState.IsValid)
            {
                var result = await VerifyAndGenerateToken(tokenRequest);
                if(result == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Error = new List<string>()
                        {
                            "InVvalid tokens"
                        },
                                Result = false
                    });
                }
                else
                {
                    return Ok(result);
                }
            }
            return BadRequest(new AuthResult()
            {
                Error = new List<string>()
                { 
                    "InVvalid Parameter"
                },
                Result = false
            });
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                _tokenValidationParameters.ValidateLifetime = false;    // for testing

                // Validation 1 -  Validation JWT Token format
                var tokeninVerifiecation = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters,
                    out var validedToken);

                // Validation 2 - Validation encryption alg
                if(validedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);
                    if(result == false)
                    {
                        return null;
                    }
                }

                // Validation 3 - Validation expiry date
                var utcExpiryDate = long.Parse(tokeninVerifiecation.Claims
                            .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
                if(expiryDate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Error = new List<string>()
                        {
                            "token has not yet expiry"
                        }
                    };
                }

                // Validation 4 - Validation existence of the token
                var storeToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);
                if(storeToken == null)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Error = new List<string>()
                        {
                            "Invalid Token"
                        }
                    };
                }

                // Validation 5 - Validation if used
                if(storeToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Error = new List<string>()
                        {
                            "Token IsUsed"
                        }
                    };
                }

                // Validation 6 - Validation if Revoked
                if (storeToken.IsReveoked)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Error = new List<string>()
                        {
                            "Invalid token"
                        }
                    };
                }

                // Validation 7 - Validation the id
                // check jti token is unique
                var jti = tokeninVerifiecation.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if(storeToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Error = new List<string>()
                        {
                            "Invalid token"
                        }
                    };
                }

                // Validation 8
                // check time token in database
                if(storeToken.ExpiryDate < DateTime.Now)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Error = new List<string>()
                        {
                            "Expired token"
                        }
                    };
                }
                
                // after checking the token then proceed to update this token is already used
                storeToken.IsUsed = true;
                _context.RefreshTokens.Update(storeToken);
                 await _context.SaveChangesAsync();

                // generate new token
                var _user = await _userManager.FindByIdAsync(storeToken.UserId);
                AuthResult tokenNew = await GenerateJwtToken(_user);

                return tokenNew;
            }
            catch (Exception e)
            {
                return new AuthResult()
                {
                    Result = false,
                    Error = new List<string>()
                        {
                            "Server Error",
                            e.Message
                        }
                };
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dateTimeVal;
        }

        private async Task<AuthResult> GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var bytes = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);

            var claims = await GetAllValidClaims(user);

            // Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),  // 5-10 minute
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), algorithm: SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                UserId = user.Id,
                Token = RandomStringGeneration(32),     // generate a refresh Token
                IsUsed = false,
                IsReveoked = false,
                AddDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResult()
            {
                Result = true,
                RefreshToken = refreshToken.Token,
                Token = jwtToken
            };
        }

        // Get all valid claim for the corresponding user
        private async Task<List<Claim>> GetAllValidClaims(IdentityUser user)
        {
            var claims = new List<Claim>()
            {
                    new Claim("type", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            };

            // getting the Claims that we have assigned to the user
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            // Get the user Role and add it to the claims
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            foreach(var userRole in userRoles)  // for Roles
            {
                IdentityRole role = await _roleManager.FindByNameAsync(userRole);

                if(role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    // Get RoleClaims in Role
                    IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(role);

                    foreach(var roleClaim in roleClaims)    // for RoleClaims
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }

        private string RandomStringGeneration(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
