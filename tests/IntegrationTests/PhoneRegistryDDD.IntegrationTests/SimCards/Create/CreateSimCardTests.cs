using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Infrastructure.EntityFramework;
using PhoneRegistryDDD.IntegrationTests.Setup;
using PhoneRegistryDDD.Warehouse.Api.Requests;
using PhoneRegistryDDD.Warehouse.Core.DAL.EntityFramework;
using PhoneRegistryDDD.Warehouse.Core.Entities;
using SuligaPawel.Common.IntegrationTest.Extensions.HttpClientExtensions;
using SuligaPawel.Common.IntegrationTest.Repositories;

namespace PhoneRegistryDDD.IntegrationTests.SimCards.Create;

[Collection(nameof(PhoneRegistryCollection))]
public class CreateSimCardTests : IClassFixture<PhoneRegistryApplicationFactory>
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

        await _httpClient.Post<IActionResult>(path, request);

        var warehouseSimCard = await _warehouseRepo.FirstOrDefault<SimCard>(x => x.Number == simNumber);
        var availabilityAssortment = await _availabilityRepo.FirstOrDefault<Assortment>(x => x.Id == warehouseSimCard.Id);
        warehouseSimCard.Should().NotBeNull();
        availabilityAssortment.Should().NotBeNull();
    }
}