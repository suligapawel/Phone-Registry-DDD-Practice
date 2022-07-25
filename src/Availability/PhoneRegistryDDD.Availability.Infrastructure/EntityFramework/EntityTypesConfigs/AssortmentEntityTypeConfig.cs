using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneRegistryDDD.Availability.Core.Entities;

namespace PhoneRegistryDDD.Availability.Infrastructure.EntityFramework.EntityTypesConfigs;

public class AssortmentEntityTypeConfig : IEntityTypeConfiguration<Assortment>
{
    public void Configure(EntityTypeBuilder<Assortment> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(x => x.Id);

        builder.Property<DateTime>("CreatedAt")
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("now()");

        builder.Property<DateTime>("UpdatedAt")
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("now()");

        builder
            .HasMany(x => x.Blocks)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Assortments", "availability");
    }
}