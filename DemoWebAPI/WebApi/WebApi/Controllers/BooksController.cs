using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Responsitories;

namespace WebApi.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookResponsitory _bookResponsitory;

        public BooksController(IBookResponsitory bookResponsitori)
        {
            _bookResponsitory = bookResponsitori;
        }

        [HttpGet]
        [Route("/getall")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                return Ok(await _bookResponsitory.GetAllBooksAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("Id")]
        
        public async Task<IActionResult> GetBooks(int Id)
        {
            var book = _bookResponsitory.GetBookAsync(Id);
           return book == null ? NotFound() : Ok(book); 
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateNewBook(BookModel model)
        {
            try
            {
                var newBookId = _bookResponsitory.CreateAsync(model);
                return CreatedAtAction(nameof(GetBooks), new { controller = "Products", newBookId }, newBookId);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
