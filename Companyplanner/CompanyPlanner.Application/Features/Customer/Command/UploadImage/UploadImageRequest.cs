using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Features.Customer.Command.UploadImage
{
    public class UploadImageRequest
    {
        /// <summary>
        /// The unique identifier of the customer or lead the images belong to.
        /// </summary>
        public int CustomerId { get; set; }   // or LeadId, depending on your design

        /// <summary>
        /// List of Base64-encoded image strings to be uploaded.
        /// </summary>
        public List<string> Images { get; set; } = new List<string>();
    }
}
