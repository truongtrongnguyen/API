using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data
{
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        public int Mahanghoa { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Tenhanghoa { get; set; }
        public string? Mota { get; set; }
        [Range(0, double.MaxValue)]
        public double Dongia { get; set; }
        public byte Giamgia { get; set; }
        public int LoaiID { get; set; }
        [ForeignKey("ForeignKey_LoaiID")]
        public Loai? Loai { get; set; }
        public ICollection<DonHangChiTiet>? DonHangChiTiets { get; set; }
    }
}
