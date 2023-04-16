using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jwt_Login_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsSetupController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ClaimsSetupController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ClaimsSetupController(AppDbContext context, UserManager<IdentityUser> userManager, ILogger<ClaimsSetupController> logger, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetAllClaims")]
        public async Task<IActionResult> GetAllClaims(string email)
        {
            // Check if User exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"The User with {email} does not exist");
                return BadRequest(new { error = $"The User with {email} does not exist" });
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            return Ok(userClaims);
        }

        [HttpPost]
        [Route("AddUserToClaim")]
        public async Task<IActionResult> AddUserToClaim(string email, string claimName, string claimValue)
        {
            // Check if User exist
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"The User with {email} does not exist");
                return BadRequest(new { error = $"The User with {email} does not exist" });
            }

            var claim = new Claim(claimName, claimValue);
            var result = await _userManager.AddClaimAsync(user, claim);

            if(result.Succeeded)
            {
                return Ok(new { result = $"User {email} has a claim  {claimName} add to them" });
            }
            else
            {
                return BadRequest(new { error = $"Unable to add {claimName} to the user {email}" });
            }
        }
    }
}
