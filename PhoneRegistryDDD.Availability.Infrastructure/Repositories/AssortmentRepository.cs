using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Availability.Infrastructure.Repositories
{
    public class AssortmentRepository : IAssortmentRepository
    {
        public Task<IEnumerable<Assortment>> GetFewBy(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateFew(IEnumerable<Assortment> assortments)
        {
            throw new NotImplementedException();
        }
    }
}
