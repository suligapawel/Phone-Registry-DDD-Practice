using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Core.Entities;

namespace PhoneRegistryDDD.Availability.Core.Repositories;

public interface IAssortmentRepository
{
    Task Add(Assortment assortment);
    Task<IReadOnlyCollection<Assortment>> GetFewBy(IEnumerable<Guid> ids);
    Task<bool> UpdateFew(IEnumerable<Assortment> assortments);
}