using WebAPI_Version.Data;
using WebAPI_Version.Models;

namespace WebAPI_Version.Services
{
    //class cho Dữ liệu chạy trên database
    public class LoaiResponsitory : ILoaiReponsitory
    {
        // TRUYỀN VÀO LÀ LoaiModels sau đó Convert thành Loai
        // LẤY RA LÀ Loai sau đó Convert thành LoaiVM
        private MyDbContext _context { get; set; }
        public LoaiResponsitory(MyDbContext context)
        {
            _context = context;
        }

        public LoaiVM Add(LoaiModels loai)
        {
            var _loai = new Loai()
            {
                TenLoai = loai.TenLoai
            };
            _context.Loais.Add(_loai);
            _context.SaveChanges();
            return new LoaiVM
            {
                MaLoai = _loai.MaLoai,
                TenLoai = _loai.TenLoai
            };
        }

        public void Delete(int id)
        {
            Loai loai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (loai != null)
            {
                _context.Loais.Remove(loai);
                _context.SaveChanges();
            }
        }

        public List<LoaiVM> GetAll()
        {
            // Do Kiểu trả về là Tên và Mã loại nên ta new nó ra một đối tượng có 2 thuộc tính đó
            var loais = _context.Loais.Select(l => new LoaiVM
            {
                TenLoai = l.TenLoai,
                MaLoai = l.MaLoai
            });
            return loais.ToList();
        }

        public LoaiVM GetByID(int id)
        {
            var loai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if(loai != null)
            {
                var _loai = new LoaiVM()
                {
                    TenLoai = loai.TenLoai,
                    MaLoai = loai.MaLoai
                };
                return _loai;
            }
            return null;
        }

        public void Update(LoaiVM loai)
        {
            Loai _loai = _context.Loais.SingleOrDefault(l => l.MaLoai == loai.MaLoai);
            _loai.TenLoai = loai.TenLoai;
            _context.SaveChanges();
        }
    }
}
