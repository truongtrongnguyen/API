using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Jwt_Login_API.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure,
                                                        U parameters,
                                                        string connections = "AppDbContext")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));

            return await connection.QueryAsync<T>(storeProcedure, parameters,
                                                    commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string storeProcedure,
                                      T parameters,
                                      string connections = "AppDbContext")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("AppDbContext"));

            await connection.ExecuteAsync(storeProcedure, parameters,
                                                    commandType: CommandType.StoredProcedure);
        }
    }
}
