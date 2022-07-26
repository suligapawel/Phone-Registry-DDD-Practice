using Microsoft.AspNetCore.Hosting;
using SuligaPawel.Common.IntegrationTest.Authorization.Users;
using SuligaPawel.Common.IntegrationTest.Extensions.WebHostBuilderExtensions;
using SuligaPawel.Common.IntegrationTest.Setup;

namespace PhoneRegistryDDD.IntegrationTests.Setup;

public sealed class PhoneRegistryApplicationFactory : CommonWebApplicationFactory<Program>
{
    private const string ConnectionString = "Host=localhost;Database=IntegrationTests;Username=postgres;Password=postgres;";
    private const string SuffixDbName = ":database:connectionString";

    protected override User DefaultUser { get; } = new(Guid.Parse("20a6762b-d784-4e94-b400-d5129db3ad80"), "psuliga");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseDatabase(
            ConnectionString,
            new[]
            {
                $"warehouse{SuffixDbName}",
                $"availability{SuffixDbName}",
                $"helpdesk{SuffixDbName}",
            });

        base.ConfigureWebHost(builder);
    }
}