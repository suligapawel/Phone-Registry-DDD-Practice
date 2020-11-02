using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Repositories
{
    //TODO
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task<Employee> GetBy(Guid id) => Employee.New(Guid.NewGuid());
        public async Task<bool> Update(Employee employee) => true;
    }
}
