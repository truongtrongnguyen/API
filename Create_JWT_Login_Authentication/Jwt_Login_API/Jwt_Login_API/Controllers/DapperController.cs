using Dapper;
using Jwt_Login_API.Models;
using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data.SqlClient;

namespace Jwt_Login_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DapperController : ControllerBase
    {
        // Link Video: https://www.youtube.com/watch?v=n0zkkoL8eNs
        // nuget: Dapper, System.Data.SqlClient

        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public DapperController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        // Store Proceduree
        [HttpGet("SP_GetAllUsersss")]
        public async Task<ActionResult<List<Usersss>>> SP_GetAllUsersss()
        {
            var result = await _context.Userssss.FromSqlRaw("GetAllUsersss").ToListAsync();

            return Ok(result);
        }

        // Store Procedure
        [HttpGet("SP_GetUsersssById")]
        public async Task<ActionResult<List<Usersss>>> SP_GetUsersssById(int Id)
        {
            var result = await _context.Userssss.FromSqlRaw($"GetAllUsersssById {Id}").ToListAsync();

            return Ok(result);
        }

        // Store Procedue 
        [HttpGet("UpdateUser")]
        public async Task<ActionResult<int>> UpdateUser([FromQuery] int Id, string validationToken)
        {
            var result = await _context.Database
                            .ExecuteSqlRawAsync($"UpdateUsersssById {Id}, {validationToken}");
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<Usersss>> DeleteUsersss(int UserId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));
            await connection.ExecuteAsync("delete from Userssss where Id = @Id", new { Id = UserId });
            return Ok(await SelectAllUsersss(connection));
        }


        [HttpGet("GetAllUsersss")]
        public async Task<ActionResult<List<Usersss>>> GetAllUserssss()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));
            IEnumerable<Usersss> userss = await SelectAllUsersss(connection);
            return Ok(userss);
        }

        [HttpGet("UserId")]
        public async Task<ActionResult<Usersss>> GetUsersss(int UserId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));
            var user = await connection.QueryFirstAsync<Usersss>("select * from Userssss where Id = @Id", new { Id = UserId });
            return Ok(user);
        }

        #region #Code tượng trưng mô phỏng cách hoạt động của Dapper, hàm CreateUserDapper được viết bên UserController
        //[HttpPost]
        //public async Task<ActionResult<List<Usersss>>> CreateUser(Usersss request)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));
        //    await connection.ExecuteAsync("insert into Userssss (Email, PassWordHash, PassWordSalt) values (@Email, @PassWordHash, @)", request);
        //    return Ok(await SelectAllUsersss(connection));
        //}

        //[HttpPut]
        //public async Task<ActionResult<List<Usersss>>> UpdateUser(Usersss request)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));
        //    await connection.ExecuteAsync("update Userssss set Email = @Email, PassWordHash = @PassWordHash, PassWordSalt = @PassWordSalt where Id = @Id)", request);
        //    return Ok(await SelectAllUsersss(connection));
        //}

        #endregion


        private async Task<IEnumerable<Usersss>> SelectAllUsersss(SqlConnection connection)
        {
            return await connection.QueryAsync<Usersss>("Select * from Userssss");
        }
    }
}
