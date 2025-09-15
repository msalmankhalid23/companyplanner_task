using CompanyPlanner.Application.Contracts.Persistence;
using CompanyPlanner.Domain;
using CompanyPlanner.Persistence.Constants;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyPlanner.Domain.Entities;

namespace CompanyPlanner.Persistence.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<int> InsertCustomer(Customer customer)
        {
            string query = @"INSERT INTO Customers (Name) 
                         VALUES (@Name);
                         SELECT CAST(SCOPE_IDENTITY() as int);";

            var result = await ExecuteAsync(async connection =>
            {
                var newId = await connection.QuerySingleAsync<int>(query, new { customer.Name });
                return newId;
            });

            return result;
        }

        public async Task<List<CustomerImage>> GetListOfImages(int customerId)
        {
            string query = QueryConstant.IMAGES_LIST;
            var result = await ExecuteAsync(async connection =>
            {
                var listofImages = await connection.QueryAsync<CustomerImage>(query, new { CustomerId  = customerId});
                return listofImages;
            });
            return result.ToList() ?? new List<CustomerImage>();
        }

        public async Task<List<CustomerImage>> GetListOfImagesByCustomerId(int customerId)
        {
            string query = @"SELECT * FROM CustomerImages WHERE CustomerId = @CustomerId";
            var result = await ExecuteAsync(async connection =>
            {
                var listofImages = await connection.QueryAsync<CustomerImage>(query, new { CustomerId = customerId });
                return listofImages;
            });
            return result.ToList() ?? new List<CustomerImage>();
        }

        public async Task<int> InsertImagesAsync(int customerId, List<string> images)
        {
            string query = @"
        INSERT INTO CustomerImages (CustomerId, ImageBase64, CreatedAt)
        VALUES (@CustomerId, @ImageBase64, @CreatedAt);";

            var now = DateTime.UtcNow;

            var result = await ExecuteAsync(async connection =>
            {
                var rows = 0;
                foreach (var image in images)
                {
                    rows += await connection.ExecuteAsync(query, new
                    {
                        CustomerId = customerId,
                        ImageBase64 = image,
                        CreatedAt = now
                    });
                }
                return rows;
            });

            return result;
        }

        public async Task<int> DeleteImageAsync(int imageId, int customerId)
        {
            string query = @"DELETE FROM CustomerImages WHERE Id = @Id AND CustomerId = @CustomerId";

            var result = await ExecuteAsync(async connection =>
            {
                return await connection.ExecuteAsync(query, new { Id = imageId, CustomerId = customerId });
            });

            return result; // rows affected
        }
    }
}
