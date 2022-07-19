using PhoneRegistryDDD.Helpdesk.Core.Dictionaries;

namespace PhoneRegistryDDD.Helpdesk.Core.Services.Devices.Policies;

public interface IDeviceChoicer
{
    DeviceClass Choice();
}