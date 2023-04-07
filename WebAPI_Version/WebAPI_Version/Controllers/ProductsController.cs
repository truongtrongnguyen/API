using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Version.Services;

namespace WebAPI_Version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IHangHoaReponsitory _ihanghoarepository { get; set; }
        public ProductsController(IHangHoaReponsitory hangHoaReponsitory)
        {
            _ihanghoarepository = hangHoaReponsitory;
        }
        [HttpGet]
        public IActionResult GetAllProduct(string search)
        {
            try
            {
               // var result = _ihanghoarepository.GetAll(search);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
