using CompanyPlanner.Application.Features.Customer.Command.DeleteImage;
using CompanyPlanner.Application.Features.Customer.Command.UploadImage;
using CompanyPlanner.Application.Features.Customer.Queries.GetImages;
using CompanyPlanner.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;

namespace CompanyPlanner.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public CustomerController(IMediator mediator, ILogger<CustomerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        /// <summary>
        /// endpoint to get list of Images
        /// </summary>
        /// <param name=""></param>
        /// <returns>A <see cref="GetImagesResponse"/> response object.</returns>
        /// <response code="200">.</response>
        /// <response code="404">If no details are found.</response>
        /// <response code="500">If there is exception while geting  the details</response>
        /// <response code="401">UnAuthorized</response>
        [HttpGet("images")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GetImagesResponse>> GetImages(int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                var response = await _mediator.Send(new GetImagesQuery(customerId));
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse()
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };

                //_logger.LogError(Constant.ERROR_TITLE, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Upload one or more images for a customer.
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="files">Uploaded image files</param>
        /// <returns></returns>
        [HttpPost("images/upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<BaseResponse>> UploadImages(
            [FromForm] int customerId,
            [FromForm] List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return BadRequest(new BaseResponse
                {
                    Success = false,
                    Message = "No files uploaded.",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            try
            {
                var base64Images = new List<string>();

                foreach (var file in files)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var bytes = ms.ToArray();
                    var base64 = Convert.ToBase64String(bytes);
                    base64Images.Add(base64);
                }
                UploadImageRequest imageRequest = new UploadImageRequest();
                imageRequest.CustomerId = customerId;
                imageRequest.Images = base64Images;
                var command = new UploadImagesCommand(imageRequest);
                var response = await _mediator.Send(command);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        /// <summary>
        /// Delete an image for a customer.
        /// </summary>
        /// <param name="customerId">The customer Id</param>
        /// <param name="imageId">The image Id</param>
        /// <returns>Status of the delete operation.</returns>
        [HttpDelete("images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResponse>> DeleteImage(int customerId, int imageId)
        {
            try
            {
                var response = await _mediator.Send(new DeleteImageCommand(customerId, imageId));

                if (!response.Success && response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

    }
}
