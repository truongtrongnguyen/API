using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Version.Models;

namespace WebAPI_Version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hanghoas = new List<HangHoa>();
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hanghoas);
        }
        [HttpGet("id")]
        public IActionResult GetById (string id)
        {
            try
            {
                var hanghoa = hanghoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound();
                }
                return Ok(hanghoa);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("id")]
        public IActionResult Edit(string id, HangHoa Edithanghoa)
        {
            // Truyền vào hàng hóa (json) và id --> Nó sẽ tìm hàng hóa trong collection theo id đó và thay đổi theo hàng hóa được truyền vào
            try
            {
                var hanghoa = hanghoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound();
                }
                hanghoa.TenHangHoa = Edithanghoa.TenHangHoa;
                hanghoa.DonGia = Edithanghoa.DonGia;
                return Ok(hanghoa);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult Create(HangHoaVM hanghoaVM)
        {
            // Truyền vào hàng hóa theo chuỗi json và nó sẽ thêm hàng hóa đó vào Collections
            var hanghoa = new HangHoa()
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hanghoaVM.TenHangHoa,
                DonGia = hanghoaVM.DonGia
            };
            hanghoas.Add(hanghoa);
            return Ok(new
            {
                Success = true,
                Data = hanghoa
            });
        }
        [HttpDelete("id")]
        public IActionResult Delete(string id)
        {
            try
            {
                var hanghoa = hanghoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound();
                }
                hanghoas.Remove(hanghoa);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
