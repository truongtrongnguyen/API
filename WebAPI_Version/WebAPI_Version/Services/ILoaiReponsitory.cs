using WebAPI_Version.Models;

namespace WebAPI_Version.Services
{
    public interface ILoaiReponsitory
    {
        public List<LoaiVM> GetAll();
        public LoaiVM GetByID(int id);
        public LoaiVM Add(LoaiModels loai);
        public void Delete(int id);
        public void Update(LoaiVM loai);
    }
}
