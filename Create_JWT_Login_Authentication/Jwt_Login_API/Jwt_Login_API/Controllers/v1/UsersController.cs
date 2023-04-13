using Jwt_Login_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_Login_API.Controllers.v1
{
    [ApiController]
    [Route("api/users")]
    [ApiVersion("1.0", Deprecated = true)]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult AllUsers()
        {
            List<UserV1> users = new List<UserV1>()
            {
                new UserV1() { Id = 1, Name = "Mohammad"},
                new UserV1() { Id = 2, Name = "Donald Duck"},
                new UserV1() { Id = 3, Name = "Neil"},

            };
            return Ok(users);
        }
    }
}
