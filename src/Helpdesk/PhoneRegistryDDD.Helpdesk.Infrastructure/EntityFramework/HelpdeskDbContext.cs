using System;
using Microsoft.EntityFrameworkCore;
using PhoneRegistryDDD.Helpdesk.Core.Entities;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;

public class HelpdeskDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();

    public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HelpdeskDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}