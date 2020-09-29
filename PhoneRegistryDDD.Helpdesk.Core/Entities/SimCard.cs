using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.ValueObjects;
using System;

namespace PhoneRegistryDDD.Helpdesk.Core.Entities
{
    public sealed class SimCard
    {
        public Guid Id { get; }

        private Device _device;

        private SimCard(Guid id)
        {
            Id = id;
        }

        private SimCard(Guid id, Device device) : this(id)
        {
            _device = device;
        }

        public static SimCard Free(Guid id) => new SimCard(id);
        public static SimCard With(Guid id, Device device) => new SimCard(id, device);

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

        internal bool Has(Device device)
        {
            if (IsFree())
                return false;

            return _device.Equals(device);
        }

        public SimSnapshot ToSnapshot() => new SimSnapshot(Id, _device.Id);
    }
}
