using Jwt_Login_API.Core.IRespositories;
using Jwt_Login_API.Models;
using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jwt_Login_API.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context, ILogger logger) : base (context, logger)
        {
        }

        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await _dbset.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<bool> Upsert(User entity)
        {
            try
            {
                var existingUser = await _dbset.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return await Add(entity);
                }

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid Id)
        {
            try
            {
                var existingUser = await _dbset.Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    _dbset.Remove(existingUser);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
                return false;
            }
        }
    }
}
