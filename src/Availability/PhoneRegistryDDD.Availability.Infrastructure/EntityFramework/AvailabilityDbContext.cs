using System;
using Microsoft.EntityFrameworkCore;
using PhoneRegistryDDD.Availability.Core.Entities;

namespace PhoneRegistryDDD.Availability.Infrastructure.EntityFramework;

public class AvailabilityDbContext : DbContext
{
    public DbSet<Assortment> Assortment => Set<Assortment>();

    public AvailabilityDbContext(DbContextOptions<AvailabilityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AvailabilityDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}