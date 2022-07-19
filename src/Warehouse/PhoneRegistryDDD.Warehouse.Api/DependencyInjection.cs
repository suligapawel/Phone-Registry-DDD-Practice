using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.API")]
namespace PhoneRegistryDDD.Warehouse.Api
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddWarehouse(this IServiceCollection services)
        {
            return services;
        }
    }
}