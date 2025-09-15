using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Domain.Entities
{
    public class CustomerImage
    {
        public int Id { get; set; }              // Primary Key
        public int CustomerId { get; set; }      // FK to Customer
        public string ImageBase64 { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
