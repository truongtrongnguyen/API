using WebAPI_Version.Data;
using WebAPI_Version.Models;

namespace WebAPI_Version.Services.AddDataHangHoa
{
    public class HangHoaReponsitoryAdd2 : IHangHoaReponsitoryAdd2
    {
        private MyDbContext _context { get; set; }
        public HangHoaReponsitoryAdd2(MyDbContext context)
        {
            _context = context;

        }
        public HangHoaAdd Add(HangHoaAdd2 hh)
        {
            var hanghoa = new Data_HangHoa()
            {
                Tenhanghoa = hh.TenHangHoa,
                Mota = hh.Mota,
                Dongia = hh.DonGia,
                Giamgia = hh.GiamGia,
                MaLoai = hh.MaLoai
            };
            _context.HangHoas.Add(hanghoa);
            _context.SaveChanges();
            return new HangHoaAdd
            {
                MaHangHoa = hanghoa.Mahanghoa,
                TenHangHoa = hanghoa.Tenhanghoa,
                Mota = hanghoa.Mota,
                DonGia = hanghoa.Dongia,
                GiamGia = hanghoa.Giamgia,
                MaLoai = hanghoa.MaLoai
            }; ;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<HangHoaAdd> GetAll2()
        {
            var hanghoa = _context.HangHoas.Select(hh => new HangHoaAdd
            {
                MaHangHoa = hh.Mahanghoa,
                TenHangHoa = hh.Tenhanghoa,
                Mota = hh.Mota,
                DonGia = hh.Dongia,
                GiamGia = hh.Giamgia,
                MaLoai = hh.MaLoai
            });
            return hanghoa.ToList();
        }

        public HangHoaAdd GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(HangHoaAdd loai)
        {
            throw new NotImplementedException();
        }
    }
}
