using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TeracromDatabase
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDbConnection _dbConnection;

        public DatabaseService(IConfiguration configuration)
        {
            _dbConnection = new SqlConnection(configuration.GetConnectionString("PS"));
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {
            return await _dbConnection.QueryAsync<T>(sql, parameters);
        }

        public async Task<IEnumerable<T>> QuerySPAsync<T>(string sql, object? parameters = null)
        {
            return await _dbConnection.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            return await _dbConnection.ExecuteAsync(sql, parameters);
        }



    }
}
