using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Features.Customer.Queries.GetImages
{
    public class GetImagesQuery : IRequest<GetImagesResponse>
    {
        public int CustomerId { get; set; }
        public GetImagesQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
