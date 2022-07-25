using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Warehouse.Core.DAL.EntityFramework;
using PhoneRegistryDDD.Warehouse.Core.DAL.Repositories;
using PhoneRegistryDDD.Warehouse.Core.DAL.Repositories.Abstracts;
using PhoneRegistryDDD.Warehouse.Core.Settings;
using SuligaPawel.Common.EF;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.Warehouse.Api")]

namespace PhoneRegistryDDD.Warehouse.Core;

internal static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
    {
        var dbSettings = config.GetSection("warehouse:database").Get<DbSettings>();

        services.AddDbContext<WarehouseDbContext>(options => options.UseNpgsql(
            dbSettings.ConnectionString,
            opt => opt.MigrationsHistoryTable("__MigrationsHistory", dbSettings.Schema)));

        return services
            .RegisterDbContextFacade<WarehouseDbContext>()
            .AddScoped<ISimCardRepository, SimCardRepository>();
    }
}