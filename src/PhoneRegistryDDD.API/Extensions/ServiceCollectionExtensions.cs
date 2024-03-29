using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Availability.Api;
using PhoneRegistryDDD.Helpdesk.Api;
using PhoneRegistryDDD.Shared.Infrastructure;
using PhoneRegistryDDD.Warehouse.Api;

namespace PhoneRegistryDDD.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddAvailability(config)
            .AddHelpdesk(config)
            .AddWarehouse(config)
            .AddShared()
            .AddApi();

        return services;
    }
}