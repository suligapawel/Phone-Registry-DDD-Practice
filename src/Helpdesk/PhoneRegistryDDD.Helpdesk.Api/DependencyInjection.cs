using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Helpdesk.Application;
using PhoneRegistryDDD.Helpdesk.Infrastructure;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.API")]

namespace PhoneRegistryDDD.Helpdesk.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddHelpdesk(this IServiceCollection services, IConfiguration config)
        => services
            .AddApplication(config)
            .AddInfrastructure(config);
}