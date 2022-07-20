using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Availability.Api;
using PhoneRegistryDDD.Availability.Application.Handlers;
using PhoneRegistryDDD.Helpdesk.Api;
using PhoneRegistryDDD.Helpdesk.Application.Handlers;
using PhoneRegistryDDD.Orchestrating;
using SuligaPawel.Common.CQRS;

namespace PhoneRegistryDDD.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddAvailability(config)
            .AddHelpdesk(config)
            .AddOrchestrating()
            .AddApi()
            .AddCqrs(typeof(TakeBackKitHandler).Assembly, typeof(UnblockAssortmentHandler).Assembly);

        return services;
    }
}