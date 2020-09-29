using PhoneRegistryDDD.Helpdesk.Dictionaries;

namespace PhoneRegistryDDD.Helpdesk.Services.Devices.Policies
{
    public interface IDeviceChoicer
    {
        DeviceClass Choice();
    }
}
