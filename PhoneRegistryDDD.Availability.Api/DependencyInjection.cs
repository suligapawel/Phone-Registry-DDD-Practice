using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Availability.Application;
using PhoneRegistryDDD.Availability.Infrastructure;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.API")]
namespace PhoneRegistryDDD.Availability.Api
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddAvailability(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddApplication(config)
                .AddInfrastructure(config);
        }
    }
}