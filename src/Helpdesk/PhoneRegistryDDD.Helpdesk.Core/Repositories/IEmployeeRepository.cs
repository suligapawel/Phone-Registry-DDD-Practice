﻿using System;
using System.Threading.Tasks;
using PhoneRegistryDDD.Helpdesk.Core.Entities;

namespace PhoneRegistryDDD.Helpdesk.Core.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> GetBy(Guid id);
    Task<bool> Add(Employee employee);
    Task<bool> Update(Employee employee);
}