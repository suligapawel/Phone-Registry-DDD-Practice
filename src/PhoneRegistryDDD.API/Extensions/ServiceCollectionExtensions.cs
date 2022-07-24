using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Availability.Api;
using PhoneRegistryDDD.Availability.Application.Events;
using PhoneRegistryDDD.Availability.Application.Handlers;
using PhoneRegistryDDD.Helpdesk.Api;
using PhoneRegistryDDD.Helpdesk.Application.Handlers;
using PhoneRegistryDDD.Shared.Infrastructure;
using SuligaPawel.Common.CQRS;

namespace PhoneRegistryDDD.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddAvailability(config)
            .AddHelpdesk(config)
            .AddApi()
            .AddCqrs(typeof(TakeBackKitHandler).Assembly, typeof(UnblockAssortmentHandler).Assembly)
            .AddSynchronousEvents(new[] { typeof(KitReturnedHandler).Assembly })
            .AddShared();

        return services;
    }
}