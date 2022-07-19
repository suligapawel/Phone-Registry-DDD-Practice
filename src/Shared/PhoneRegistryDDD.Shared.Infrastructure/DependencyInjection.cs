using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.API")]

namespace PhoneRegistryDDD.Shared.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration config) => services;

    public static IHostBuilder AddModuleSettings(this IHostBuilder builder)
    {
        return builder.ConfigureAppConfiguration((ctx, cfg) =>
        {
            var moduleSettings = Directory.EnumerateFiles(
                $"{ctx.HostingEnvironment.ContentRootPath}/../",
                $"module.*.json",
                SearchOption.AllDirectories);

            foreach (var settings in moduleSettings)
            {
                cfg.AddJsonFile(settings);
            }
        });
    }
}