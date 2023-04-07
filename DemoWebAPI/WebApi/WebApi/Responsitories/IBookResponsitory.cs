using WebApi.Models;

namespace WebApi.Responsitories
{
    public interface IBookResponsitory
    {
        public Task<List<Book>> GetAllBooksAsync();
        public Task<Book> GetBookAsync(int Id);
        public Task CreateAsync(BookModel model);
        public Task UpdateAsync(int Id, BookModel model);
        public Task DeleteAsync(int Id);

    }
}
