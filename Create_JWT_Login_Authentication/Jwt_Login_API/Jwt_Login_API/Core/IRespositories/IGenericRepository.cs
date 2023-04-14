namespace Jwt_Login_API.Core.IRespositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(Guid id);
        Task<bool> Delete(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Upsert(T entity);
    }
}
