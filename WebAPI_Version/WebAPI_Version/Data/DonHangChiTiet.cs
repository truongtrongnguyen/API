namespace WebAPI_Version.Data
{
    public class DonHangChiTiet
    {
        public Guid MaHH { get; set; }
        public Guid MaDH { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        // RealtionShip
        public DonHang DonHang { get; set; }
        public Data_HangHoa HangHoa {get; set;}
    }
}
