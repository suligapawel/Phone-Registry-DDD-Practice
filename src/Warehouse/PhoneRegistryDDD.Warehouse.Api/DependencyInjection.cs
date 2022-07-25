using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Warehouse.Core;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.API")]

namespace PhoneRegistryDDD.Warehouse.Api
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddWarehouse(this IServiceCollection services, IConfiguration config)
            => services.AddCore(config);
    }
}