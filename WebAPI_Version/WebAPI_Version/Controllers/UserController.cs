
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI_Version.Data;
using WebAPI_Version.Models;
using WebAPI_Version.Services;

namespace WebAPI_Version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSettings _appSettings;

        public UserController(MyDbContext context, IOptions<AppSettings> appsetting)
        {
            _context = context;
            _appSettings = appsetting.Value;
        }

        [HttpPost]
        [Route("/validate")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _context.NguoiDung.SingleOrDefault(x => x.UserName == model.UserName && x.PassWork == model.PassWork);
            if (user == null)
            {
                return Ok(new APIResponse()
                {
                    Success = false,
                    Message = "Invalid UserName/PassWord"
                });
            }
            // Cấp token
            var token = GenerateToKen(user);
            return Ok(new APIResponse()
            {
                Success = true,
                Message = "Authentication Success", 
                Data = token
            });
        }

        private TokenModel GenerateToKen(NguoiDung nguoidung)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokeDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, nguoidung.HoTen),
                    new Claim(JwtRegisteredClaimNames.Email, nguoidung.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, nguoidung.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userName", nguoidung.UserName),
                    new Claim("Id", nguoidung.ID.ToString()),

                    // Các Role
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokeDescription);
            var accessToken =  jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();  // Random Token để Refresh

            // Lưu DataBase
            var refreshTokens = new RefreshToken()
            {
                JwtId = token.Id,
                Token = refreshToken,
                IsUsed = false,
                UserId = nguoidung.ID,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };
            _context.RefreshTokens.Add(refreshTokens);
            _context.SaveChanges();

            return new TokenModel()
            {
                AccessToKen = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rgn = RandomNumberGenerator.Create())
            {
                rgn.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        [HttpPost]
        [Route("/renewtoken")]
        public IActionResult ReNewToken2(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var SecretkeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var validationParameters = new TokenValidationParameters
            {
                // Tự cấp ToKen, nếu sài dịch vụ ngoài thì là true và phải chỉ ra đường dẫn,  config tới dịch vụ mình chọn
                ValidateIssuer = false,
                ValidateAudience = false,

                // Ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(SecretkeyBytes),

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false   // Không kiểm tra token hết hạn
            };
            try
            {
                // Check 1: Accsess ToKen Valid Format(Check xem có đúng định dạng ban đầu hay không)
            var tokenInVerification = jwtTokenHandler.ValidateToken(model.AccessToKen, validationParameters, out var ValidateToken);

                // Check 2: Check Thuật toán có giống với ban đầu hay không
                if (ValidateToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, // Không cần ký giống lúc khởi tạo ban dầu
                                                        StringComparison.InvariantCulture);     // Không phân biệt hoa thường
                    if (!result)
                    {
                        return Ok(new APIResponse()
                        {
                            Success = false,
                            Message = "Invalid Token"
                        });
                    }
                }

                // Check 3: Check AccessToken expire?? (kiểm tra xem token còn thời hạn hay không)
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(z => z.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        Message = "Access Token has not yet expired"
                    });
                }

                // Check 4: Check ReefreshToken exists in DB
                var storeToken = _context.RefreshTokens.SingleOrDefault(x => x.Token == model.RefreshToken);
                if (storeToken == null)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        Message = "Refresh Token does not exists"
                    });
                }

                // Check 5: Check RefreshToken is used/Revoked???
                if (storeToken.IsUsed.GetValueOrDefault())
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        Message = "Refresh Token has been Used"
                    });
                }
                if (storeToken.IsRevoked)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        Message = "Refresh Token has been Revoked"
                    });
                }

                // Check 6: AccessToken id == JwtId in RefreshToken
                var jti = tokenInVerification.Claims.FirstOrDefault(z => z.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storeToken.JwtId != jti)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        Message = "Token does not Match"
                    });
                }

                // Update token is used
                storeToken.IsUsed = true;
                storeToken.IsRevoked = true;
                _context.RefreshTokens.Update(storeToken);
                _context.SaveChanges();

                // Create New Token
                var user = _context.NguoiDung.SingleOrDefault(x => x.ID == storeToken.UserId);
                var token = GenerateToKen(user);
                return Ok(new APIResponse()
                {
                    Success = true,
                    Message = "Authentication Success",
                    Data = token
                });


                return Ok(new APIResponse()
                {
                    Success = true
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new APIResponse()
                {
                    Success = false,
                    Message = "Something went wrong"
                });
            }

        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTimeInterval;
        }

        //private string GenerateToKen(NguoiDung nguoidung)
        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();
        //    var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

        //    var tokeDescription = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim(ClaimTypes.Name, nguoidung.HoTen),
        //            new Claim(ClaimTypes.Email, nguoidung.Email),
        //            new Claim("userName", nguoidung.UserName),
        //            new Claim("Id", nguoidung.ID.ToString()),

        //            // Các Role

        //            new Claim("TokenId", Guid.NewGuid().ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
        //    };

        //    var token = jwtTokenHandler.CreateToken(tokeDescription);
        //    return jwtTokenHandler.WriteToken(token);
        //}
    }
}
