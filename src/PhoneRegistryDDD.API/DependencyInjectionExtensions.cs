using Microsoft.Extensions.DependencyInjection;
using SuligaPawel.Common.Exceptions.Middlewares;

namespace PhoneRegistryDDD.API;

internal static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services) =>
        services.AddScoped<ExceptionMiddleware>();
}