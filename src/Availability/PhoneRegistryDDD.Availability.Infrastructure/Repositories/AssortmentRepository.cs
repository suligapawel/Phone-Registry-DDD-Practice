using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;
using PhoneRegistryDDD.Availability.Infrastructure.EntityFramework;

namespace PhoneRegistryDDD.Availability.Infrastructure.Repositories;

public class AssortmentRepository : IAssortmentRepository
{
    private readonly AvailabilityDbContext _dbContext;

    public AssortmentRepository(AvailabilityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Assortment assortment)
        => await _dbContext.AddAsync(assortment);

    public async Task<IReadOnlyCollection<Assortment>> GetFewBy(IEnumerable<Guid> ids)
        => await _dbContext.Assortment
            .Include(x => x.Blocks)
            .ThenInclude(x => x.Owner)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

    public async Task<bool> UpdateFew(IEnumerable<Assortment> assortments)
    {
        ArgumentNullException.ThrowIfNull(assortments);

        foreach (var assortment in assortments)
        {
            _dbContext.Assortment.Update(assortment);
        }

        return await Task.FromResult(true);
    }
}