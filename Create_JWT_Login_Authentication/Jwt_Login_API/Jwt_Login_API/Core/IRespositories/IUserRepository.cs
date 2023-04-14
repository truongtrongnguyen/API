using Jwt_Login_API.Models;
using Microsoft.AspNetCore.Identity;

namespace Jwt_Login_API.Core.IRespositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

    }
}
