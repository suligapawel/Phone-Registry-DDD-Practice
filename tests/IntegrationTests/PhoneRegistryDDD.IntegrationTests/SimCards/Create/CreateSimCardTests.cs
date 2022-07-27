using FluentAssertions;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Infrastructure.EntityFramework;
using PhoneRegistryDDD.IntegrationTests.Setup;
using PhoneRegistryDDD.Warehouse.Api.Requests;
using PhoneRegistryDDD.Warehouse.Core.DAL.EntityFramework;
using PhoneRegistryDDD.Warehouse.Core.Entities;
using SuligaPawel.Common.IntegrationTest.Extensions.HttpClientExtensions;
using SuligaPawel.Common.IntegrationTest.Repositories;
using SuligaPawel.Common.IntegrationTest.Repositories.Cache;

namespace PhoneRegistryDDD.IntegrationTests.SimCards.Create;

[Collection(nameof(PhoneRegistryCollection))]
public sealed class CreateSimCardTests : IAsyncLifetime
{
    private readonly HttpClient _httpClient;
    private readonly IRepository _availabilityRepo;
    private readonly IRepository _warehouseRepo;

    public CreateSimCardTests(PhoneRegistryApplicationFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);

        _httpClient = factory.CreateClient();
        _availabilityRepo = factory.CreateRepository<AvailabilityDbContext>();
        _warehouseRepo = factory.CreateRepository<WarehouseDbContext>();
    }

    [Fact]
    public async Task Should_add_new_sim_card()
    {
        const string path = "warehouse/simCards";
        const string simNumber = "777888999";
        var request = new CreateSimCardRequest(simNumber, "1234", "12345678");

        var id = await _httpClient.Post<Guid>(path, request);

        var warehouseSimCard = (await _warehouseRepo.FirstOrDefault<SimCard>(x => x.Number == simNumber)).AsCached(_warehouseRepo);
        var availabilityAssortment =
            (await _availabilityRepo.FirstOrDefault<Assortment>(x => x.Id == warehouseSimCard.Id)).AsCached(_availabilityRepo);
        warehouseSimCard.Id.Should().Be(id);
        availabilityAssortment.Id.Should().Be(id);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _availabilityRepo.CleanUp();
        await _warehouseRepo.CleanUp();
    }
}