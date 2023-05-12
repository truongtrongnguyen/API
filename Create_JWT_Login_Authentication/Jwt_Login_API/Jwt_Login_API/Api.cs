using Jwt_Login_API.Data;
using Jwt_Login_API.Models;

namespace Jwt_Login_API
{
    public static class Api
    {
        public static void ConfigureApi(this WebApplication app)
        {
            // All of API endpoint mapping
            app.MapGet("/GetAllCategoryDapper", GetCategorys);
            app.MapGet("/GetCategoryDapperById/{id}", GetCategoryById);
            app.MapPost("/InsertCategoryDapper", InsertCategoryDapper);
            app.MapPut("/UpdateCategoryDapper", UpdateCategoryDapper);
            app.MapDelete("/DeleteCategoryDapper", DeleteCategoryDapper);
        }

        private static async Task<IResult> GetCategorys(ICategoryData _data)
        {
            try
            {
                return Results.Ok(await _data.GetAllCategory());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetCategoryById(int id, ICategoryData _data)
        {
            try
            {
                var result = await _data.GetCategory(id);

                if (result == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> InsertCategoryDapper (CategoryMediator category, ICategoryData _data)
        {
            try
            {
                await _data.InsertCategory(category);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> UpdateCategoryDapper(CategoryMediator category, ICategoryData _data)
        {
            try
            {
                await _data.UpdateCategory(category);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteCategoryDapper(int id, ICategoryData _data)
        {
            try
            {
                await _data.DeleteCategory(id);
                return Results.Ok();    
            }
            catch(Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
