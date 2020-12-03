using Microsoft.EntityFrameworkCore;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;
using System;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HelpdeskContext _dbContext;

        public EmployeeRepository(HelpdeskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Employee> GetBy(Guid id)
            => _dbContext.Employees.Include(x => x.SimCards).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public async Task<bool> Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}
