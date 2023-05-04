using Dapper;
using Jwt_Login_API.Models;
using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Jwt_Login_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Link Video: https://www.youtube.com/watch?v=2Q9Uh-5O8Sk

        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public UserController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword request)
        {
            var user = _context.Userssss.FirstOrDefault(x => x.PassWordResetToken == request.Token);
            if (user == null)
            {
                return BadRequest("Invalid Token");
            }

            CreatePassWordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PassWordHash = passwordHash;
            user.PassWordSalt = passwordSalt;
            user.PassWordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok("Password successfuly reset");
        }

        [HttpPost("Forgot-Password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = _context.Userssss.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return BadRequest("User not founb");
            }

            user.PassWordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return Ok("You may now reset your password");
        }

        [HttpPost("Verified")]
        public async Task<IActionResult> Verified(string token)
        {
            var user = _context.Userssss.FirstOrDefault(x => x.VerificationToken == token);
            if(user == null)
            {
                return BadRequest("token invalid");
            }

            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok("user has verifies");
        }

        [HttpPost("Loggin")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = _context.Userssss.Where(x => x.Email == request.Email).FirstOrDefault();
            if (user ==  null)
            {
                return BadRequest("User not found");
            }

            // check user has verify or not verify
            if (user.VerifiedAt == null)
            {
                return BadRequest("User not verify");
            }

            if (!VerifyPasswordHash(request.Password, user.PassWordHash, user.PassWordSalt))
            {
                return BadRequest("Password is incorrect");
            }

            return Ok($"Welcom back, {user.Email}");
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_context.Userssss.Any(x => x.Email == request.Email))
            {
                return BadRequest("User already exixst");
            }

            CreatePassWordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new Usersss()
            {
                Email = request.Email,
                PassWordHash = passwordHash,
                PassWordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };

            _context.Userssss.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User successfuly create");
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        private void CreatePassWordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }





        [HttpPost("CreateUserDapper")]
        public async Task<ActionResult<List<Usersss>>> CreateUser(UserRegisterRequest request)
        {
            CreatePassWordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new Usersss()
            {
                Email = request.Email,
                PassWordHash = passwordHash,
                PassWordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };

            using var connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));
            await connection.ExecuteAsync("insert into Userssss (Email, PassWordHash, PassWordSalt, VerificationToken) values (@Email, @PassWordHash, @PassWordSalt, @VerificationToken)", user);
            return Ok(await SelectAllUsersss(connection));
        }

        private async Task<IEnumerable<Usersss>> SelectAllUsersss(SqlConnection connection)
        {
            return await connection.QueryAsync<Usersss>("Select * from Userssss");
        }
    }
}
