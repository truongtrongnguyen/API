namespace Jwt_Login_API.Models
{
    public class Usersss
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PassWordHash { get; set; } = new byte[32];
        public byte[] PassWordSalt { get; set; } = new byte[32];
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string?  PassWordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}
