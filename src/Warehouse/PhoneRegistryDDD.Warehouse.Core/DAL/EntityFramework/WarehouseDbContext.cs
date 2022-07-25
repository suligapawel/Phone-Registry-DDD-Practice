using System;
using Microsoft.EntityFrameworkCore;
using PhoneRegistryDDD.Warehouse.Core.Entities;

namespace PhoneRegistryDDD.Warehouse.Core.DAL.EntityFramework;

public class WarehouseDbContext : DbContext
{
    public DbSet<SimCard> SimCards => Set<SimCard>();

    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}