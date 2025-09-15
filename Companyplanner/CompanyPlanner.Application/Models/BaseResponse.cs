using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Models
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
            StatusCode = HttpStatusCode.OK;
        }
        public BaseResponse(string message)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? ValidationErrors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
