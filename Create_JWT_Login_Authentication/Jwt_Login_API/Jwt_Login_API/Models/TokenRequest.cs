using System.ComponentModel.DataAnnotations;

namespace Jwt_Login_API.Models
{
    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
