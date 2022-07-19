using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;

namespace PhoneRegistryDDD.Availability.Infrastructure.Repositories;

// TODO
public class AssortmentRepository : IAssortmentRepository
{
    public async Task<IEnumerable<Assortment>> GetFewBy(IEnumerable<Guid> ids) => await Task.FromResult(new[] { Assortment.New() });
    public async Task<bool> UpdateFew(IEnumerable<Assortment> assortments) => await Task.FromResult(true);
}