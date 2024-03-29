using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;
using PhoneRegistryDDD.Helpdesk.Infrastructure.Repositories;
using PhoneRegistryDDD.Helpdesk.Infrastructure.Settings;
using SuligaPawel.Common.EF;

[assembly: InternalsVisibleTo("PhoneRegistryDDD.Helpdesk.Api")]

namespace PhoneRegistryDDD.Helpdesk.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var dbSettings = config.GetSection("helpdesk:database").Get<DbSettings>();

        services.AddDbContext<HelpdeskDbContext>(options => options.UseNpgsql(
            dbSettings.ConnectionString,
            opt => opt.MigrationsHistoryTable("__MigrationsHistory", dbSettings.Schema)));

        services
            .RegisterDbContextFacade<HelpdeskDbContext>()
            .AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}