using System;

namespace PhoneRegistryDDD.Helpdesk.Core.Entities.Devices
{
    public class Device
    {
        public Guid Id { get; private set; }

        [Obsolete("For EF", true)]
        public Device() { }

        public Device(Guid id)
        {
            Id = id;
        }

        internal bool IsSameTypeAs(Device device)
        {
            return GetType() == device.GetType();
        }

        public override bool Equals(object obj)
        {
            return obj is Device device &&
                   Id.Equals(device.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
