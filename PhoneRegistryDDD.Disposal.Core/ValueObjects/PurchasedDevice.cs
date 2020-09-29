using System;

namespace PhoneRegistryDDD.Disposal.Core.ValueObjects
{
    public class PurchasedDevice
    {
        private Guid Id { get; }

        public PurchasedDevice(Guid id)
        {
            Id = id;
        }

        public bool IsSameDeviceAs(UsedDevice usedDevice)
            => Id.Equals(usedDevice.Id);

        public override bool Equals(object obj)
            => obj is PurchasedDevice device && Id.Equals(device.Id);

        public override int GetHashCode() => HashCode.Combine(Id);
    }
}
