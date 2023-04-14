using Jwt_Login_API.Core.IConfiguration;
using Jwt_Login_API.Core.IRespositories;
using Jwt_Login_API.Core.Repositories;
using JWT_Login_Authentication.Models;

namespace Jwt_Login_API.Models
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        public IUserRepository Users { get; private set;}
        public UnitOfWork(AppDbContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger("logs");

            Users = new UserRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
