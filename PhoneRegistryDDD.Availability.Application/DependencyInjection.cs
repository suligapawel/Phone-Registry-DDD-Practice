using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Availability.Application.Events;
using PhoneRegistryDDD.Availability.Application.Handlers;
using PhoneRegistryDDD.Availability.Core.Commands;
using PhoneRegistryDDD.Shared.Abstractions.Commands;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.Availability.Api")]

namespace PhoneRegistryDDD.Availability.Application
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddScoped<ICommandHandler<UnblockAssortmentCommand, AssortmentUnblocked>, UnblockAssortmentHandler>();
        }
    }
}