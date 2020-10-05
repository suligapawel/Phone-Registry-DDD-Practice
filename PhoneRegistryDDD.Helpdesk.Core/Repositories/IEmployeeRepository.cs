using PhoneRegistryDDD.Helpdesk.Core.Entities;
using System;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetBy(Guid id);
        Task<bool> Update(Employee employee);
    }
}
