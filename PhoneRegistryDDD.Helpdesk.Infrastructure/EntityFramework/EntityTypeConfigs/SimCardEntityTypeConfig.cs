using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework.EntityTypeConfigs
{
    internal class SimCardEntityTypeConfig : IEntityTypeConfiguration<SimCard>
    {
        public void Configure(EntityTypeBuilder<SimCard> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.Device);
            builder.OwnsOne(x => ((Smartphone)x.Device), x => x.ToTable("Smartphones", "helpdesk"));

            builder.ToTable("Simcards", "helpdesk");
        }
    }
}
