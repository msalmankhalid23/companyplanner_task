using CompanyPlanner.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Features.Customer.Command.DeleteImage
{
    public class DeleteImageCommand : IRequest<BaseResponse>
    {
        public int CustomerId { get; set; }
        public int ImageId { get; set; }

        public DeleteImageCommand(int customerId, int imageId)
        {
            CustomerId = customerId;
            ImageId = imageId;
        }

    }
}
