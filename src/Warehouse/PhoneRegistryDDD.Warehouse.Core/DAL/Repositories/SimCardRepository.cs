using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneRegistryDDD.Warehouse.Core.DAL.EntityFramework;
using PhoneRegistryDDD.Warehouse.Core.DAL.Repositories.Abstracts;
using PhoneRegistryDDD.Warehouse.Core.Entities;

namespace PhoneRegistryDDD.Warehouse.Core.DAL.Repositories;

public class SimCardRepository : ISimCardRepository
{
    private readonly WarehouseDbContext _dbContext;

    public SimCardRepository(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(SimCard simCard)
        => await _dbContext.SimCards.AddAsync(simCard);

    public async Task<bool> ExistsAlready(string number)
        => await _dbContext.SimCards.AnyAsync(x => x.Number == number);
}