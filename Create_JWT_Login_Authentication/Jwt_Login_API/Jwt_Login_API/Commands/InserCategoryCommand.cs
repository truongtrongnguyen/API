using Jwt_Login_API.Models;
using MediatR;

namespace Jwt_Login_API.Commands
{
    // Nên chia ra một thư mục tên là Command 
    public record InsertCategoryCommand(string CategoryName) : IRequest<CategoryMediator>;


    //public class InserCategoryCommand: IRequest<CategoryMediator>
    //{
    //    public InserCategoryCommand(string categoryName)
    //    {
    //        CategoryName = categoryName;
    //    }
    //    public string CategoryName { get; set; }
    //}
}
