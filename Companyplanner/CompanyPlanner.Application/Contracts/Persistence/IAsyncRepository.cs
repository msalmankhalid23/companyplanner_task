using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Application.Contracts.Persistence
{
    public interface IAsyncRepository
    {
        Task<T> ExecuteAsync<T>(Func<IDbConnection, Task<T>> func);
    }
}
