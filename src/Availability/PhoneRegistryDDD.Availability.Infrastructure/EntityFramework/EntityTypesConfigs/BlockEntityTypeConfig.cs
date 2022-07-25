using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneRegistryDDD.Availability.Core.ValueObjects;

namespace PhoneRegistryDDD.Availability.Infrastructure.EntityFramework.EntityTypesConfigs;

public class BlockEntityTypeConfig : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Property<Guid>("Id");
        builder.HasKey("Id");

        builder.Ignore(x => x.Owner);
        builder.OwnsOne(x => x.Owner, a => a.Property(x => x.Id).HasColumnName("OwnerId"));

        builder.ToTable("Blocks", "availability");
    }
}