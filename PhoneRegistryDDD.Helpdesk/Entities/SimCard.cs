using PhoneRegistryDDD.Helpdesk.Entities.Devices;

namespace PhoneRegistryDDD.Helpdesk.Entities
{
    public sealed class SimCard
    {
        private Device _device;

        private SimCard() { }
        private SimCard(Device device)
        {
            _device = device;
        }

        public static SimCard Free() => new SimCard();
        public static SimCard With(Device device) => new SimCard(device);

        internal bool IsFree()
        {
            return _device == null;
        }

        internal void SetDevice(Device device)
        {
            _device = device;
        }

        internal bool HasDeviceOfSameTypeAs(Device device)
        {
            if (IsFree())
                return false;

            return _device.IsSameTypeAs(device);
        }
    }
}
