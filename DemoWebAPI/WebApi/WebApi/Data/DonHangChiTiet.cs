using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data
{
    public class DonHangChiTiet
    {
        [Key]
        public int DonHangChiTietID { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }

        // RealtionShip
        public int DonHangID { get; set; }
        [ForeignKey("ForeignKey_MaDonHang")]
        public DonHang? DonHang { get; set; }

        public int HangHoaID { get; set; }
        [ForeignKey("ForeignKey_MaHangHoa")]
        public HangHoa? HangHoa {get; set;}
    }
}
