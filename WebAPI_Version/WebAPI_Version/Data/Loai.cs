using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Version.Data
{
    [Table("Loai")]
    public class Loai
    {
        [Required]
        [MaxLength(50)]
        public string? TenLoai { get; set; }    
        [Key]
        public int MaLoai { get; set; }
        public virtual ICollection<Data_HangHoa> ListHangHoa { get; set; }
    }
}
