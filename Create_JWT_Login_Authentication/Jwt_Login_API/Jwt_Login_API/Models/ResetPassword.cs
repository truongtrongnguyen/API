using System.ComponentModel.DataAnnotations;

namespace Jwt_Login_API.Models
{
    public class ResetPassword
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 character, dute!")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string MyProperty { get; set; } = string.Empty;
    }
}
