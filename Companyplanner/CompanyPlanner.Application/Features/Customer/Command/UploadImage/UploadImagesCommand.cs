using CompanyPlanner.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Features.Customer.Command.UploadImage
{
    public class UploadImagesCommand : IRequest<BaseResponse>
    {
        public UploadImageRequest UploadImageRequest_;
        public UploadImagesCommand(UploadImageRequest request)
        {
            UploadImageRequest_ = request;
        }
    }
}
