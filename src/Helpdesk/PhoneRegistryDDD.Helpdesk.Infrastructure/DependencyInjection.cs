using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;
using PhoneRegistryDDD.Helpdesk.Infrastructure.Repositories;
using PhoneRegistryDDD.Helpdesk.Infrastructure.Settings;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.Helpdesk.Api")]

namespace PhoneRegistryDDD.Helpdesk.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var dbSettings = config.GetSection("helpdesk:database").Get<DbSettings>();

        services.AddDbContext<HelpdeskDbContext>(options => options.UseNpgsql(
            dbSettings.ConnectionString,
            options => options.MigrationsHistoryTable("__MigrationsHistory", dbSettings.Schema)));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}