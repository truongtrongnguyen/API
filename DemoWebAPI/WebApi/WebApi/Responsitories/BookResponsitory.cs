using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Responsitories
{
    public class BookResponsitory : IBookResponsitory
    {
        private readonly AppDbContext _context;
        private IMapper _autoMapper;

        public BookResponsitory(AppDbContext context, IMapper autoMapper)
        {
            _context = context;
            _autoMapper = autoMapper;
        }

        public async Task CreateAsync(BookModel model)
        {
            var book = _autoMapper.Map<Book>(model);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var book = _context.Books.Find(Id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = _context.Books.ToListAsync();
            //return _autoMapper.Map<List<BookModel>>(books);
            return books.Result;
        }

        public async Task<Book> GetBookAsync(int Id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == Id);
            //return _autoMapper.Map<BookModel>(book);
            if(book != null)
            {
                return book;
            }
            return null;
        }

        public Task UpdateAsync(int Id, BookModel model)
        {
            if(Id == model.Id)
            {
                var book = _autoMapper.Map<Book>(model);
                var bookUpdate = _context.Books.Find(Id);
                if( bookUpdate != null)
                {
                    _context.Books.Update(book);
                }
            }
            return Task.CompletedTask;
        }
    }
}
