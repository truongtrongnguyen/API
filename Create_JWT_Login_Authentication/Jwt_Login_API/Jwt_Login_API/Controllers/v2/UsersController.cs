using Jwt_Login_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_Login_API.Controllers.v2
{
    [ApiController]
    //[Route("api/v{version:apiVersion}/users")]      // https://localhost:7053/api/v2/users
    [Route("api/users")]
    [ApiVersion("2.0")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult AllUsers()
        {
            List<UserV2> users = new List<UserV2>()
            {
                new UserV2() { Id = Guid.NewGuid(), Name = "Mohammad"},
                new UserV2() { Id = Guid.NewGuid(), Name = "Donald Duck"},
                new UserV2() { Id = Guid.NewGuid(), Name = "Neil"},

            };
            return Ok(users);
        }
    }
}
