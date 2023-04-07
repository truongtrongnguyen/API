using System.ComponentModel.DataAnnotations;
using WebAPI_Version.Data;

namespace WebAPI_Version.Models
{
    public class RefreshToken
    {
        [Key]
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public NguoiDung? NguoiDung { get; set; }
        public string? Token { get; set; }  // Nội dung của RefreshToken
        public string? JwtId { get; set; }  // Mã Token
        public bool?IsUsed { get; set; }   // Đã được sử dụng hay chưa
        public bool IsRevoked { get; set; } // Đã được thu hồi hay chưa
        public DateTime? IssuedAt { get; set; } // Ngày tạo
        public DateTime? ExpiredAt { get; set; }    // Ngày Hết hạn
    }
}
