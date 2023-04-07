using System.ComponentModel.DataAnnotations;

namespace Jwt_Login_API.Models
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
