using Jwt_Login_API.Models;
using MediatR;

namespace Jwt_Login_API.Queries
{
    // Nên chia ra một thư mục tên là Queries 
    public record GetCategoryByIdQuery(int Id) : IRequest<CategoryMediator>;

    // Tương đương với 1 dòng ở trên
    //public class GetCategoryByIdQueryClass : IRequest<CategoryMediator>;
    //{
    //    public int Id { get; set; }
    //    public GetCategoryByIdClass(int id)
    //    {
    //        Id = id;
    //    }
    //}
}
