using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Version.Data
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(20)]
        public string PassWork { get; set; }
        [Required]
        public string HoTen { get; set; }
        [Required]  
        public string Email { get; set; }
    }
}
