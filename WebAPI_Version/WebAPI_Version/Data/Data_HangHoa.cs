using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Version.Data
{
    // Link Youtube: https://www.youtube.com/watch?v=PKkKCv7Lno0&list=PLE5Bje814fYbhdwSHiHN9rlwJlwJ2YD3t&index=2
    [Table("HangHoa")]
    public class Data_HangHoa
    {
        [Key]
        public Guid Mahanghoa { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Tenhanghoa { get; set; }
        public string? Mota { get; set; }
        [Range(0, double.MaxValue)]
        public double Dongia { get; set; }
        public byte Giamgia { get; set; }
        public int MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public Loai Loai { get; set; }
        public ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
        public Data_HangHoa()
        {
            DonHangChiTiets = new List<DonHangChiTiet>();
        }

    }
}
