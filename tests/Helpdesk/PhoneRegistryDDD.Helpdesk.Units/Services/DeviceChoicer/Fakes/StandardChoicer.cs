using PhoneRegistryDDD.Helpdesk.Core.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Core.Services.Devices.Policies;

namespace PhoneRegistryDDD.Helpdesk.Units.Services.DeviceChoicer.Fakes;

internal class StandardChoicer : IDeviceChoicer
{
    public DeviceClass Choice() => DeviceClass.Standard;
}