using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Version.Models;
using WebAPI_Version.Services;

namespace WebAPI_Version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiUseInterface : ControllerBase
    {
        private ILoaiReponsitory _iloaiReponsitory { get; set; }
        // Sau khi Add xong thì ta tiến hành đăng ký dịch vụ 
        public LoaiUseInterface(ILoaiReponsitory iloaiReponsitory)
        {
            _iloaiReponsitory = iloaiReponsitory;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_iloaiReponsitory.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("id")]
        public IActionResult GetByID(int id)
        {
            try
            {
                var loai = _iloaiReponsitory.GetByID(id);
                if(loai!=null)
                {
                    return Ok(loai);
                }
                return null;
            }
            catch
            {
                return BadRequest();

            }
        }
        [HttpPut]
        public IActionResult Update(int id, LoaiVM loai)
        {
           if(id != loai.MaLoai)
            {
                return BadRequest();
            }
           try
            {
                _iloaiReponsitory.Update(loai);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult Create(LoaiModels loai)
        {
            try
            {
                if(loai != null)
                {
                    return Ok(_iloaiReponsitory.Add(loai));
                }
                return null;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        public IActionResult Delete (int id)
        {
            try
            {
                _iloaiReponsitory.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
