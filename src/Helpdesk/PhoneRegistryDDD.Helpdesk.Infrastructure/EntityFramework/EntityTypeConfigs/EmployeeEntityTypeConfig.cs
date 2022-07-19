using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneRegistryDDD.Helpdesk.Core.Entities;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework.EntityTypeConfigs;

internal class EmployeeEntityTypeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property<DateTime>("CreatedAt")
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETDATE()");

        builder.Property<DateTime>("UpdatedAt")
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("GETDATE()");

        builder.HasMany(x => x.SimCards);

        builder.ToTable("Employees", "helpdesk");
    }
}