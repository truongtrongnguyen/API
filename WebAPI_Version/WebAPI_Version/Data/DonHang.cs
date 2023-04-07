using System.ComponentModel.DataAnnotations;

namespace WebAPI_Version.Data
{
    public class DonHang
    {
        public Guid MaDH { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public TinhTrangDonHang TinhTrangDonHang { get; set; }
        public string NguoiNhan { get; set; }
        public string DiachiGiao { get; set; }
        public string SoDienThoai { get; set; }
        public ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
        // Muốn cho nó không trả về null mà trả về danh sách rỗng nên ta phải tạo hàm khởi tạo 
        public DonHang ()
        {
            DonHangChiTiets = new List<DonHangChiTiet>();
        }
    }
    public enum TinhTrangDonHang
    {
        New = 0, Payment= 1,Complete = 2, Cancel = -1
    }
}
