using CompanyPlanner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Contracts.Persistence
{
    public interface ICustomerRepository
    {
        Task<List<CustomerImage>> GetListOfImages(int customerId);
        Task<List<CustomerImage>> GetListOfImagesByCustomerId(int customerId);
        Task<int> InsertImagesAsync(int customerId, List<string> images);
        Task<int> DeleteImageAsync(int imageId, int customerId);
    }
}
