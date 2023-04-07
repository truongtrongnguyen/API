
using Jwt_Login_API.Models;
using JWT_Login_Authentication.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AuthenticationController(UserManager<IdentityUser> userManager, IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
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
                var new_user = new IdentityUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Name
                };

                var is_created = await _userManager.CreateAsync(new_user, requestDto.Password);

                if (is_created.Succeeded)
                {
                    // generate a Token
                    var token = GenerateJwtToken(new_user);
                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = token
                    });
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

                var jwtToken = GenerateJwtToken(existsing_user);

                return Ok(new AuthResult()
                {
                    Result = true,
                    Token = jwtToken
                });
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

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var bytes = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);

            // Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("type", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.Now.AddSeconds(50),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), algorithm: SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;

        }
    }
}
