using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework.EntityTypeConfigs
{
    internal class DeviceEntityTypeConfig : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }

    internal class SmartphoneEntityTypeConfig : DeviceEntityTypeConfig
    {
        public void Configure(EntityTypeBuilder<Smartphone> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Smartphones", "helpdesk");
        }
    }
}
