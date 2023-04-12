using System.ComponentModel.DataAnnotations;

namespace Jwt_Login_API.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsReveoked { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
