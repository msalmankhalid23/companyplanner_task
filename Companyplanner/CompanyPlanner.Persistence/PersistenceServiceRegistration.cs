using CompanyPlanner.Application.Contracts.Persistence;
using CompanyPlanner.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPlanner.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository), typeof(BaseRepository));
            services.AddRepositories(typeof(BaseRepository).Assembly);
            return services;
        }

        public static void AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            var repositoryTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));

            foreach (var implementationType in repositoryTypes)
            {
                var interfaceType = implementationType.GetInterfaces().FirstOrDefault(f => f.Name == $"I{implementationType.Name}");
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, implementationType);
                }
            }
        }
    }
}
