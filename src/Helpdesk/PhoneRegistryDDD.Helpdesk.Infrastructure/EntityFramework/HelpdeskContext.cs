using System;
using Microsoft.EntityFrameworkCore;
using PhoneRegistryDDD.Helpdesk.Core.Entities;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;

public class HelpdeskContext : DbContext
{
    public DbSet<Employee> Employees { get; init; }

    public HelpdeskContext(DbContextOptions<HelpdeskContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HelpdeskContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}