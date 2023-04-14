using Jwt_Login_API.Core.IConfiguration;
using Jwt_Login_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_Login_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUnitOfWork _unitOfWork;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if(ModelState.IsValid)
            {
                user.Id = new Guid();
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { user.Id }, user);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetItem (Guid id)
        {
            var user = await _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return NotFound();  
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _unitOfWork.Users.All();
            return Ok(users);
        }

        [HttpPost("id")]
        public async Task<IActionResult> UpdateItem(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _unitOfWork.Users.Upsert(user);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var user = await _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(user);
        }
    }
}
