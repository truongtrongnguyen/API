    namespace WebAPI_Version.Models
{
    // Link Youtube: https://www.youtube.com/watch?v=PKkKCv7Lno0&list=PLE5Bje814fYbhdwSHiHN9rlwJlwJ2YD3t&index=2
    public class HangHoaVM  // Thêm dữ liệu vào
    {
        public string TenHangHoa { get; set; }
        public Double DonGia { get; set; }
    }
    public class HangHoa : HangHoaVM
    {
        public Guid MaHangHoa { get; set; }
    }
    public class HangHoaModels  // Lấy dữ liệu ra
    {
        public Guid MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public Double DonGia { get; set; }  
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
    }
    public class HangHoaAdd
    {
        public Guid MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string? Mota { get; set; }
        public Double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public int MaLoai { get; set; }
    }
    public class HangHoaAdd2
    {
        public string TenHangHoa { get; set; }
        public string? Mota { get; set; }
        public Double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public int MaLoai { get; set; }
    }
}
