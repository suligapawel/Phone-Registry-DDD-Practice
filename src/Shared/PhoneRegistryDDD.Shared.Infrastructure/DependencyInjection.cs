using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuligaPawel.Common.CQRS;
using SuligaPawel.Common.EF;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.API")]

namespace PhoneRegistryDDD.Shared.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(x => x.FullName?.StartsWith("PhoneRegistry", StringComparison.OrdinalIgnoreCase) ?? false)
            .ToArray();

        return services
            .AddCqrs(assemblies)
            .AddSynchronousEvents(assemblies)
            .DecorateCommandHandlersWithTransaction();
    }

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