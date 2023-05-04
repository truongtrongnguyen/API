using Dapper;
using Jwt_Login_API.Models;
using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Jwt_Login_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagingUsersssController : ControllerBase
    {
        private readonly IConfiguration _config;
        public PagingUsersssController(IConfiguration config)
        {
            _config = config;;
        }

        // Nên tạo repository
        [HttpGet]
        [ProducesResponseType(typeof(Usersss), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Usersss), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Usersss>>> ListUser([FromQuery] UrlQuery urlQuery)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));

            StringBuilder query = new StringBuilder();

            query.Append("select *  ");
            query.Append("from Userssss ");
            
            // Điều kiện luôn đúng để khi ta có thêm điều kiện dễ dàng hơn
            query.Append("WHERE 1 = 1 ");   
            
            if (!string.IsNullOrEmpty(urlQuery.keyword))
            {
                query.Append("And ( ");
                query.Append("Lower( Email ) like N'%' + @Keyword + '%' ");
                query.Append(") ");
            }

            query.Append("order by Id desc ");
            query.Append("offset (@Page - 1 ) * @PageSize rows ");
            query.Append("fetch next @PageSize rows only ");

            var temp = query.ToString();

            var result = await connection.QueryAsync<Usersss>(temp, new
            {
                Page = urlQuery.Page,
                PageSize = urlQuery.PageSize,
                Keyword = urlQuery.keyword.ToLower()
            });

            // Count users
            var totalUser = await connection.QueryFirstOrDefaultAsync<int>("select count(Id) from Userssss");

            return Ok(new
            {
                result = result,
                totalUser = totalUser
            });
        }
    }
}
