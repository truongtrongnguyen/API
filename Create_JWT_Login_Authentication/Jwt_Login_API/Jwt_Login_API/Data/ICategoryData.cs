using Jwt_Login_API.Models;

namespace Jwt_Login_API.Data
{
    public interface ICategoryData
    {
        Task DeleteCategory(int id);
        Task<IEnumerable<CategoryMediator>> GetAllCategory();
        Task<CategoryMediator?> GetCategory(int id);
        Task InsertCategory(CategoryMediator category);
        Task UpdateCategory(CategoryMediator category);
    }
}