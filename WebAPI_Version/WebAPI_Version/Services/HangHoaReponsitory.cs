using Microsoft.EntityFrameworkCore;
using WebAPI_Version.Data;
using WebAPI_Version.Models;

namespace WebAPI_Version.Services
{
    public class HangHoaReponsitory : IHangHoaReponsitory
    {
        private MyDbContext _context { get; set; }
        private  int PAGE_SIZE  = 5;
        public HangHoaReponsitory(MyDbContext context)
        {
            _context = context;
        }
        public List<HangHoaModels> GetAll(string? search, int? from, int? to, string? sortBy,int page = 1)
        {
            var allProduct = _context.HangHoas.Include(hh => hh.Loai).AsQueryable();   // Truy vấn nhưng chưa chạy xuống database lấy dữ liệu


            #region Sort
            // Default Sort By Name     
            allProduct = allProduct.OrderBy(hh => hh.Tenhanghoa);

            switch (sortBy)
            {
                case "ten_desc":
                    {
                        allProduct = allProduct.OrderByDescending(hh => hh.Tenhanghoa);
                        break;
                    }
                case "gia_asc":
                    {
                        allProduct = allProduct.OrderBy(hh => hh.Dongia);
                        break;
                    }
                case "gia_desc":
                    {
                        allProduct = allProduct.OrderByDescending(hh => hh.Dongia);
                        break;
                    }
            }
            #endregion

            #region Filter
            if (!string.IsNullOrEmpty(search))
            {
                allProduct = allProduct.Where(hh => hh.Tenhanghoa.Contains(search));
            }
            if (from != null)
            {
                allProduct = allProduct.Where(hh => hh.Dongia >= from);
            }
            if (to != null)
            {
                allProduct.Where(hh => hh.Dongia <= to);
            }
            #endregion

            #region Page
            //allProduct = allProduct.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            #endregion

            //// Lấy ra những thứ cần lấy trong lớp HangHoa
            //var result = allProduct.Select(hh => new HangHoaModels
            //{
            //    MaHangHoa = hh.Mahanghoa,
            //    TenHangHoa = hh.Tenhanghoa,
            //    DonGia = hh.Dongia,
            //    MaLoai = hh.MaLoai,
            //    TenLoai = hh.Loai.TenLoai
            //});
            //return result.ToList();

            var result = PaginatedList<Data_HangHoa>.Create(allProduct, page, PAGE_SIZE);
            return result.Select(hh => new HangHoaModels
            {
                MaHangHoa = hh.Mahanghoa,
                TenHangHoa = hh.Tenhanghoa,
                DonGia = hh.Dongia,
                MaLoai = hh.MaLoai,
                TenLoai = hh.Loai.TenLoai
            }).ToList();
        }
    }
}
