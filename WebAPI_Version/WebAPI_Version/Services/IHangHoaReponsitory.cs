using WebAPI_Version.Models;

namespace WebAPI_Version.Services
{
    public interface IHangHoaReponsitory
    {
        public List<HangHoaModels> GetAll(string? search, int? from, int? to, string? sortBy, int page = 1);
    }
}
