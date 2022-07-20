using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.Helpdesk.Api")]

namespace PhoneRegistryDDD.Helpdesk.Application;

internal static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config) =>
        services;
}