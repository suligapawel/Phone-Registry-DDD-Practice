using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<Employee> GetBy(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
