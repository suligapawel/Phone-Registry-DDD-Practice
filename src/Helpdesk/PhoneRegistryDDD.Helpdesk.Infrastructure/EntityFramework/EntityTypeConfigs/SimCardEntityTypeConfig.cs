using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneRegistryDDD.Helpdesk.Core.Entities;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework.EntityTypeConfigs;

internal class SimCardEntityTypeConfig : IEntityTypeConfiguration<SimCard>
{
    public void Configure(EntityTypeBuilder<SimCard> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.Device);
        builder.OwnsOne(x => x.Device, a => a.ToTable("Devices"));
        builder.ToTable("Simcards", "helpdesk");
    }
}