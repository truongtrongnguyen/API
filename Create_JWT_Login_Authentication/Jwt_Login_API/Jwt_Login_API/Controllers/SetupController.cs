using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

// https://www.youtube.com/watch?v=eVxzuOxWEiY&list=PLcvTyQIWJ_ZpumOgCCify-wDY_G-Kx34a&index=6

namespace Jwt_Login_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetupController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<SetupController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SetupController(AppDbContext context, UserManager<IdentityUser> userManager, ILogger<SetupController> logger, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            // Check if the role exist
            var roleExist = await _roleManager.RoleExistsAsync(name);

            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));
                // We need to check if the role has been added successfuly
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The Role {name} has been addded successfuly");
                    return Ok(new { result = $"The Role {name} has been added successfuly" });
                }
                else
                {
                    _logger.LogError($"The Role {name} has not been addded");
                    return BadRequest(new { error = $"The Role {name} has not been added" });
                }
            }
            return BadRequest(new { error = "Role already exist" });
        }

        [HttpGet]
        [Route("GetAllUser")]
        public IActionResult GetAllUser()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            // Check if User exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"The User with {email} does not exist");
                return BadRequest(new { error = $"The User with {email} does not exist" });
            }

            // Check if Role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                _logger.LogInformation($"The Role {roleName} does not exist");
                return BadRequest(new { error = $"The Role {roleName} does not exist" });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            // Check if the user is assigned to the role successfuly
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = "Success, user has been added to the role"
                });
            }
            else
            {
                _logger.LogInformation($"The user was not abel to be added to the role");
                return BadRequest(new { error = $"The user was not abel to be added to the role" });
            }
        }

        [HttpGet]
        [Route("GetUserRole")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            // Check if the Email is valid
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"The User with {email} does not exist");
                return BadRequest(new { error = $"The User with {email} does not exist" });
            }
            // return the role
            var role = await _userManager.GetRolesAsync(user);

            return Ok(role);
        }

        [HttpPost]
        [Route("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string role)
        {
            // Check if User exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"The User with {email} does not exist");
                return BadRequest(new { error = $"The User with {email} does not exist" });
            }

            // Check if Role exist
            var roleExist = await _roleManager.RoleExistsAsync(role);

            if (!roleExist)
            {
                _logger.LogInformation($"The Role {role} does not exist");
                return BadRequest(new { error = $"The Role {role} does not exist" });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role);

            if(result.Succeeded)
            {
                return Ok(new { Success = $"User {email}has been remove from role {role}" });
            }

            return BadRequest(new { error = $"Unable to remove User {email} from role {role}" });
        }
    }
}
