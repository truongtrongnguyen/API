using Jwt_Login_API.Core.IRespositories;

namespace Jwt_Login_API.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}
