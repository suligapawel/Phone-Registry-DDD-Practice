using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;
using PhoneRegistryDDD.Helpdesk.Infrastructure.Repositories;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.Helpdesk.Api")]

namespace PhoneRegistryDDD.Helpdesk.Infrastructure
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<HelpdeskContext>(options =>
                options.UseSqlServer(config.GetConnectionString("helpdesk")));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}