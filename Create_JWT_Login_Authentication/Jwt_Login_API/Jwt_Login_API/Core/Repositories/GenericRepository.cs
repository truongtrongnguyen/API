using Jwt_Login_API.Core.IRespositories;
using JWT_Login_Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Jwt_Login_API.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected DbSet<T> _dbset;
        protected readonly ILogger _logger;

        public GenericRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;

            this._dbset = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await _dbset.ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbset.FindAsync(id);
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _dbset.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
