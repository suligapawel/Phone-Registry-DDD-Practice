using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneRegistryDDD.Warehouse.Core.Entities;

namespace PhoneRegistryDDD.Warehouse.Core.DAL.Configuration;

public class SimCardConfig : IEntityTypeConfiguration<SimCard>
{
    public void Configure(EntityTypeBuilder<SimCard> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Number)
            .IsRequired()
            .HasMaxLength(9);

        builder
            .Property(x => x.Pin)
            .IsRequired()
            .HasMaxLength(12);

        builder
            .Property(x => x.Puk)
            .IsRequired()
            .HasMaxLength(8);
    }
}