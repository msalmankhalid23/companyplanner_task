using CompanyPlanner.Application.Contracts.Persistence;
using CompanyPlanner.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Features.Customer.Queries.GetImages
{
    public class GetImagesHandler : IRequestHandler<GetImagesQuery, GetImagesResponse>
    {

        private readonly ICustomerRepository _repository;
        public GetImagesHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<GetImagesResponse> Handle(GetImagesQuery request, CancellationToken cancellationToken)
        {
            var response = new GetImagesResponse();

            try
            {
                var result = await _repository.GetListOfImages(request.CustomerId);

                if (result == null || !result.Any())
                {
                    response.CustomerImages = new List<CustomerImage>();
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"No images found for customer {request.CustomerId}.";
                    return response;
                }

                response.CustomerImages = result;
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{result.Count} image(s) retrieved successfully.";
                return response;
            }
            catch (Exception ex)
            {
                response.CustomerImages = new List<CustomerImage>();
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = $"Error while retrieving images: {ex.Message}";
                return response;
            }
        }
    }
}
