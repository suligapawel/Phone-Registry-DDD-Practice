using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Orchestrating.Abstractions.Kit;
using PhoneRegistryDDD.Orchestrating.Kit;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.API")]

namespace PhoneRegistryDDD.Orchestrating;

internal static class DependencyInjection
{
    public static IServiceCollection AddOrchestrating(this IServiceCollection services) =>
        services.AddScoped<ITakeBackKitFacade, TakeBackKitService>();
}