using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Availability.Core.Repositories;
using PhoneRegistryDDD.Availability.Infrastructure.EntityFramework;
using PhoneRegistryDDD.Availability.Infrastructure.Repositories;
using PhoneRegistryDDD.Availability.Infrastructure.Settings;
using SuligaPawel.Common.EF;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.Availability.Api")]

namespace PhoneRegistryDDD.Availability.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var dbSettings = config.GetSection("helpdesk:database").Get<DbSettings>();

        services.AddDbContext<AvailabilityDbContext>(options => options.UseNpgsql(
            dbSettings.ConnectionString,
            opt => opt.MigrationsHistoryTable("__MigrationsHistory", dbSettings.Schema)));

        services
            .RegisterDbContextFacade<AvailabilityDbContext>()
            .AddScoped<IAssortmentRepository, AssortmentRepository>();

        return services;
    }
}