using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Availability.Core.Repositories;
using PhoneRegistryDDD.Availability.Infrastructure.Repositories;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.Availability.Api")]
namespace PhoneRegistryDDD.Availability.Infrastructure
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            return services.AddScoped<IAssortmentRepository, AssortmentRepository>();
        }
    }
}