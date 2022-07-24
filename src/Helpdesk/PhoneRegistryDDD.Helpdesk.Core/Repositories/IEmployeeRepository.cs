using System;
using System.Threading.Tasks;
using PhoneRegistryDDD.Helpdesk.Core.Entities;

namespace PhoneRegistryDDD.Helpdesk.Core.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> GetBy(Guid id);
    Task Add(Employee employee);
    Task Update(Employee employee);
}