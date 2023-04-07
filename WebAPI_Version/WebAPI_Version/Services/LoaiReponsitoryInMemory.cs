using WebAPI_Version.Models;

namespace WebAPI_Version.Services
{
    /// <summary>
    /// Class tạo dữ liệu chạy trên máy
    /// </summary>
    public class LoaiReponsitoryInMemory : ILoaiReponsitory
    {
        private static List<LoaiVM> Loais = new List<LoaiVM>()
        {
            new LoaiVM (){MaLoai = 1, TenLoai ="Tu lanh"},
            new LoaiVM (){MaLoai = 2, TenLoai ="Ti vi"},
            new LoaiVM (){MaLoai = 3, TenLoai ="May giac"},
            new LoaiVM (){MaLoai = 4, TenLoai ="Quat gio"},
            new LoaiVM (){MaLoai = 5, TenLoai ="Dieu hoa"},
        };
        public LoaiVM Add(LoaiModels loai)
        {
            var _loai = new LoaiVM
            {
                MaLoai = Loais.Max(l => l.MaLoai) + 1,
                TenLoai = loai.TenLoai
            };
            Loais.Add(_loai);
            return _loai;
        }

        public void Delete(int id)
        {
            var loai = Loais.SingleOrDefault(l => l.MaLoai == id);
            Loais.Remove(loai);
        }

        public List<LoaiVM> GetAll()
        {
            return Loais;
        }

        public LoaiVM GetByID(int id)
        {
            var loai = Loais.SingleOrDefault(l => l.MaLoai == id);
            return loai;
        }

        public void Update(LoaiVM loai)
        {
            var _loai = Loais.SingleOrDefault(l => l.MaLoai == loai.MaLoai);
            if (_loai != null)
            {
                _loai.TenLoai = loai.TenLoai;
            }
        }
    }
}
