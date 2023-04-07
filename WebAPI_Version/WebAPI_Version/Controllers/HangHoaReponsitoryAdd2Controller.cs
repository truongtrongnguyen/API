using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Version.Data;
using WebAPI_Version.Models;
using WebAPI_Version.Services;
using WebAPI_Version.Services.AddDataHangHoa;

namespace WebAPI_Version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaReponsitoryAdd2Controller : ControllerBase
    {
        private IHangHoaReponsitoryAdd2 _ihangHoaReponsitoryAdd2 { get; set; }
        private IHangHoaReponsitory _ihanghoaReponsitory { get; set; }

        public HangHoaReponsitoryAdd2Controller(IHangHoaReponsitoryAdd2 ihangHoaReponsitoryAdd2, IHangHoaReponsitory ihanghoaReponsitory)
        {
            _ihangHoaReponsitoryAdd2 = ihangHoaReponsitoryAdd2;
            _ihanghoaReponsitory = ihanghoaReponsitory;
        }
        //[HttpGet]
        //public IActionResult GetAll2()
        //{
        //    return Ok(_ihangHoaReponsitoryAdd2.GetAll2());
        //}
        [HttpGet("search")]
        public IActionResult GetAll(string? search, int? from, int? to, string? sortBy, int page = 1)
        {
            var result = _ihanghoaReponsitory.GetAll(search, from, to, sortBy, page);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(HangHoaAdd2 hh)
        {
            if(hh != null)
            {
                return Ok(_ihangHoaReponsitoryAdd2.Add(hh));
            }
            return null;
        }
    }
}
