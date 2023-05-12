using Dapper;
using FluentValidation;
using FluentValidation.Results;
using Jwt_Login_API.Commands;
using Jwt_Login_API.Infrastructure;
using Jwt_Login_API.Migrations;
using Jwt_Login_API.Models;
using Jwt_Login_API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data.SqlClient;
using System.Text;

namespace Jwt_Login_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestMediatRController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        public TestMediatRController(IMediator mediator
                        , IConfiguration config
                        )
        {
            _mediator = mediator;
            _config = config;
        }

        // Dùng MediatR 
        [HttpPost("InsertCategoryCommand")]
        public async Task<CategoryMediator> InsertCategoryCommand(CategoryMediator request)                                      
        {
            var model = new InsertCategoryCommand(request.Name);
            return await _mediator.Send(model);
        }

        // Dùng MediatR
        [HttpGet("GetCategoryById")]
        public async Task<CategoryMediator> GetCategoryById(int Id)
        {
            return await _mediator.Send(new GetCategoryByIdQuery(Id));
        }


        [HttpGet("GetAllCategory")]
        [ProducesResponseType(typeof(Response<List<CategoryMediator>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponseDefault>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCategory()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));

            var builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append("from CategoryMediators ");
            var query = builder.ToString();

            var result = await connection.QueryAsync<CategoryMediator>(query);

            if (result != null)
            {
                var temp = new Response<List<CategoryMediator>>()
                {
                    State = true,
                    Message = "00000000",
                    @object = result.ToList()
                };
                return Ok(temp);
            }

            return NotFound(new Response<ResponseDefault>()
            {
                State = false,
                Message = "00000001"
            });

        }

        // Nên tạo repository
        [HttpGet("PagingCategory")]
        [ProducesResponseType(typeof(Usersss), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Usersss), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Usersss>>> PagingCategory([FromQuery] UrlQuery urlQuery)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));

            StringBuilder query = new StringBuilder();

            query.Append("select *  ");
            query.Append("from CategoryMediators ");

            // Điều kiện luôn đúng để khi ta có thêm điều kiện dễ dàng hơn
            query.Append("WHERE 1 = 1 ");


            query.Append("order by Id desc ");
            query.Append("offset (@Page - 1 ) * @PageSize rows ");
            query.Append("fetch next @PageSize rows only ");

            var temp = query.ToString();

            var result = await connection.QueryAsync<CategoryMediator>(temp, new
            {
                Page = urlQuery.Page,
                PageSize = urlQuery.PageSize,
                Keyword = urlQuery.keyword.ToLower()
            });

            // Count users
            var totalUser = await connection.QueryFirstOrDefaultAsync<int>("select count(Id) from Userssss");

            var model = new Response<Pagination<CategoryMediator>>()
            {
                State = true,
                Message = "00000000",
                @object = new Pagination<CategoryMediator>()
                {
                    Total = totalUser,
                    Items = result.ToList()
                }
            };

            return Ok(model);
        }
    }
}
