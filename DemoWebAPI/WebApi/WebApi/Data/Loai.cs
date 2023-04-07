using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data
{
    [Table("Loai")]
    public class Loai
    {
        [Required]
        [MaxLength(50)]
        public string? TenLoai { get; set; }    
        [Key]
        public int MaLoai { get; set; }
        public virtual ICollection<HangHoa>? ListHangHoa { get; set; }
    }
}
