using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Persistence.Constants
{
    public class QueryConstant
    {
        public const string IMAGES_LIST = @"select * from CustomerImages where CustomerId = @CustomerId";
    }
}
