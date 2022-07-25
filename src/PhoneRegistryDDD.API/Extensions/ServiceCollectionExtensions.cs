using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Availability.Api;
using PhoneRegistryDDD.Availability.Application.Events;
using PhoneRegistryDDD.Availability.Application.Handlers;
using PhoneRegistryDDD.Helpdesk.Api;
using PhoneRegistryDDD.Helpdesk.Application.Handlers;
using PhoneRegistryDDD.Shared.Infrastructure;
using PhoneRegistryDDD.Warehouse.Api;
using PhoneRegistryDDD.Warehouse.Core.CQRS.Commands.SimCards.Create;
using PhoneRegistryDDD.Warehouse.Core.Events;
using SuligaPawel.Common.CQRS;

namespace PhoneRegistryDDD.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddAvailability(config)
            .AddHelpdesk(config)
            .AddWarehouse(config)
            .AddApi()
            .AddCqrs(typeof(TakeBackKitHandler).Assembly, typeof(UnblockAssortmentHandler).Assembly, typeof(CreateSimCardHandler).Assembly)
            .AddSynchronousEvents(new[] { typeof(KitReturnedHandler).Assembly, typeof(SimCardCreated).Assembly })
            .AddShared();

        return services;
    }
}