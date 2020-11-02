using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Availability.Infrastructure.Repositories
{
    //TODO
    public class AssortmentRepository : IAssortmentRepository
    {
        public async Task<IEnumerable<Assortment>> GetFewBy(IEnumerable<Guid> ids) => new Assortment[] { Assortment.New() };
        public async Task<bool> UpdateFew(IEnumerable<Assortment> assortments) => true;
    }
}
