﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Core.Entities;

namespace PhoneRegistryDDD.Availability.Core.Repositories;

public interface IAssortmentRepository
{
    Task<IEnumerable<Assortment>> GetFewBy(IEnumerable<Guid> ids);
    Task<bool> UpdateFew(IEnumerable<Assortment> assortments);
}