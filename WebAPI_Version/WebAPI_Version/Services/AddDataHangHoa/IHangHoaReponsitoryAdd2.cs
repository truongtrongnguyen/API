using WebAPI_Version.Models;

namespace WebAPI_Version.Services.AddDataHangHoa
{
    public interface IHangHoaReponsitoryAdd2
    {
        public List<HangHoaAdd> GetAll2();
        //  public HangHoaAdd GetByID(int id);
        public HangHoaAdd Add(HangHoaAdd2 hh);
      //  public void Delete(int id);
      //  public void Update(HangHoaAdd loai);
    }
}
