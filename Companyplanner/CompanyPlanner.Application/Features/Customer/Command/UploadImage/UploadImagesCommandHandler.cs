using CompanyPlanner.Application.Contracts.Persistence;
using CompanyPlanner.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Features.Customer.Command.UploadImage
{
    class UploadImagesCommandHandler : IRequestHandler<UploadImagesCommand, BaseResponse>
    {
        private readonly ICustomerRepository _repository;
        public UploadImagesCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponse> Handle(UploadImagesCommand request, CancellationToken cancellationToken)
        {

            // Step 1: Get current images count for this customer
            var existingImages = await _repository.GetListOfImagesByCustomerId(request.UploadImageRequest_.CustomerId);

            if (existingImages.Count + request.UploadImageRequest_.Images.Count > 10)
            {
                return new BaseResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "A maximum of 10 images are allowed per customer."
                };
            }

            var inserted = await _repository.InsertImagesAsync(request.UploadImageRequest_.CustomerId,request.UploadImageRequest_.Images);
            return new BaseResponse
            {
                Success = inserted > 0,
                StatusCode = HttpStatusCode.OK,
                Message = inserted > 0
                ? $"{inserted} image(s) uploaded successfully."
                : "No images were uploaded."
            };
        }
    }
}
