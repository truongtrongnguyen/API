using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HangHoasController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<HangHoa> GetAll()
        {
            return _context.HangHoas.ToList();
        }

        [HttpPost]
        public IActionResult Add(HangHoa hanghoaModel)
        {
            try
            {
                _context.HangHoas.Add(hanghoaModel);
                _context.SaveChanges();
                return Ok(hanghoaModel);
            }
            catch
            {
                return BadRequest(hanghoaModel);
            }
        }
    }
}
