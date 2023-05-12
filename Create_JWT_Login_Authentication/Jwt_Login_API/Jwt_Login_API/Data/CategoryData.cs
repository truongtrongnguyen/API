using Jwt_Login_API.DbAccess;
using Jwt_Login_API.Models;

namespace Jwt_Login_API.Data
{
    public class CategoryData : ICategoryData
    {
        private readonly ISqlDataAccess _dataAccess;

        public CategoryData(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Read data
        public Task<IEnumerable<CategoryMediator>> GetAllCategory()
        {
            return _dataAccess.LoadData<CategoryMediator, dynamic>("dbo.spCategory_GetAllCategory", new { });
        }

        // Read data
        public async Task<CategoryMediator?> GetCategory(int id)
        {
            var result = await _dataAccess.LoadData<CategoryMediator, dynamic>("dbo.spCategory_GetCategory", new { Id = id });

            return result.FirstOrDefault();
        }

        // Insert data
        public Task InsertCategory(CategoryMediator category)
        {
            _dataAccess.SaveData("dbo.spCategory_InsertCategory", new { category.Name });

            return Task.CompletedTask;
        }

        // Update data 
        public Task UpdateCategory(CategoryMediator category)
        {
            _dataAccess.SaveData("dbo.spCategory_UpdateCategory", category);
            return Task.CompletedTask;
        }

        // Delete data
        public Task DeleteCategory(int id)
        {
            _dataAccess.SaveData("dbo.spCategory_DeleteCategory", new { Id = id });
            return Task.CompletedTask;
        }

    }
}
