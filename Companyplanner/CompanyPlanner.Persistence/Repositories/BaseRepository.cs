using CompanyPlanner.Application.Contracts.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Persistence.Repositories
{
    public class BaseRepository : IAsyncRepository
    {
        protected readonly IConfiguration _configuration;
        protected readonly string _dbConnectionString;
        public BaseRepository(IConfiguration configuration)
        {

            _configuration = configuration;
            _dbConnectionString = configuration["ConnectionStrings:DefaultConnection"] ?? "";
        }

        public async Task<T> ExecuteAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            await using var connection = new SqlConnection(_dbConnectionString);
            await connection.OpenAsync();
            try
            {
                return await func(connection);
            }
            catch (Exception ex)
            {
                await connection.CloseAsync();
                throw;
            }
        }
    }
}
