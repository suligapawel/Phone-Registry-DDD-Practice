using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Helpdesk.Application.Handlers;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Shared.Abstractions.Commands;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.Helpdesk.Api")]

namespace PhoneRegistryDDD.Helpdesk.Application
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            return services.AddScoped<ICommandHandler<TakeBackKitCommand, bool>, TakeBackKitHandler>();
        }
    }
}