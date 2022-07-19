using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly HelpdeskDbContext _dbDbContext;

    public EmployeeRepository(HelpdeskDbContext dbDbContext)
    {
        _dbDbContext = dbDbContext;
    }

    public Task<Employee> GetBy(Guid id)
        => _dbDbContext.Employees.Include(x => x.SimCards).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<bool> Add(Employee employee)
    {
        _dbDbContext.Employees.Add(employee);
        return await _dbDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Employee employee)
    {
        _dbDbContext.Employees.Update(employee);
        return await _dbDbContext.SaveChangesAsync() > 0;
    }
}