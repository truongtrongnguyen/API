using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Version.Data;
using WebAPI_Version.Models;

namespace WebAPI_Version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisssController : ControllerBase
    {
        private MyDbContext _context { get; set; }

        public LoaisssController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var ListLoai = _context.Loais.ToList();
            if(ListLoai ==null)
            {
                return NotFound();
            }
            return Ok(ListLoai);
        }
        [HttpGet("id")]
        public IActionResult GetByMaLoai(int MaLoai)
        {
            var Loai = _context.Loais.SingleOrDefault(loai => loai.MaLoai == MaLoai);
            if(Loai == null)
            {   
                return NotFound();
            }
            return Ok(Loai);
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateLoai(LoaiModels loaimodels)
        {
            // Do MaLoai là tự tăng nên ta tạo ra một Class chỉ có tên loại (như một Class Temp)
            try
            {
                var Loai = new Loai()
                {
                    TenLoai = loaimodels.TenLoai
                };
                _context.Loais.Add(Loai);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, Loai);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult UpdateLoaiPut(int id, LoaiModels loaimodels)
        {
            var Loai = _context.Loais.SingleOrDefault(loai => loai.MaLoai == id);
            try
            {
                if (Loai == null)
                {
                    return NotFound();
                }
                Loai.TenLoai = loaimodels.TenLoai;
                _context.SaveChanges();
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
