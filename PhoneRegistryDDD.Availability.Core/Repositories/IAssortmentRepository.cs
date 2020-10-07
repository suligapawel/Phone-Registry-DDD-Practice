using PhoneRegistryDDD.Availability.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Availability.Core.Repositories
{
    public interface IAssortmentRepository
    {
        Task<IEnumerable<Assortment>> GetFewBy(IEnumerable<Guid> ids);
        Task<bool> UpdateFew(IEnumerable<Assortment> assortments);
    }
}
