using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneRegistryDDD.Warehouse.Core.Entities;

namespace PhoneRegistryDDD.Warehouse.Core.DAL.Configuration;

public class SmartphoneConfig : IEntityTypeConfiguration<Smartphone>
{
    public void Configure(EntityTypeBuilder<Smartphone> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Brand)
            .IsRequired()
            .HasMaxLength(32);

        builder
            .Property(x => x.Imei)
            .IsRequired()
            .HasMaxLength(15);

        builder
            .Property(x => x.Model)
            .IsRequired()
            .HasMaxLength(64);
    }
}