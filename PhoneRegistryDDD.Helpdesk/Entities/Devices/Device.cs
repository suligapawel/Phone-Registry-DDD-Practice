namespace PhoneRegistryDDD.Helpdesk.Entities.Devices
{
    public abstract class Device
    {
        internal bool IsSameTypeAs(Device device)
        {
            return GetType() == device.GetType();
        }
    }
}
