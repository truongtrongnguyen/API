using Jwt_Login_API.Validations.Commond;

namespace Jwt_Login_API.Models
{
    //public record CreateCategoryRequest(
    //                                string Name,
    //                                string Descriptions,
    //                                bool IsActive,
    //                                DateTime DateCreate,
    //                                DateTime DateUpdate);
      public record CreateCategoryRequest(
                                    string Name,
                                    string Descriptions,
                                    bool IsActive,
                                 CreateCategoryDetails CreateCategoryDetails);
}
