namespace Jwt_Login_API.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters, string connections = "AppDbContext");
        Task SaveData<T>(string storeProcedure, T parameters, string connections = "AppDbContext");
    }
}