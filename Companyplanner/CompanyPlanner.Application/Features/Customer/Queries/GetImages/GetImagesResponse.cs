using CompanyPlanner.Application.Models;
using CompanyPlanner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Features.Customer.Queries.GetImages
{
    public class GetImagesResponse : BaseResponse
    {
        public List<CustomerImage> CustomerImages { get; set; } = new List<CustomerImage>();
    }
}
