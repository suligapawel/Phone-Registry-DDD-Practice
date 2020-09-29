using System;

namespace PhoneRegistryDDD.Helpdesk.Entities.Devices
{
    public abstract class Device
    {
        public Guid Id { get; }

        protected Device(Guid id)
        {
            Id = id;
        }

        internal bool IsSameTypeAs(Device device)
        {
            return GetType() == device.GetType();
        }
    }
}
