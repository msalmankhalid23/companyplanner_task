using CompanyPlanner.Application.Contracts.Persistence;
using CompanyPlanner.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Features.Customer.Command.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, BaseResponse>
    {
        private readonly ICustomerRepository _repository;
        public DeleteImageCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponse> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {

            var response = new BaseResponse();

            try
            {
                var rowsAffected = await _repository.DeleteImageAsync(request.ImageId, request.CustomerId);

                if (rowsAffected == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"No image found with Id {request.ImageId} for customer {request.CustomerId}.";
                    return response;
                }

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Image deleted successfully.";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = $"Error deleting image: {ex.Message}";
                return response;
            }
        }
    }
}
