using System;

namespace PhoneRegistryDDD.Disposal.ValueObjects
{
    public sealed class UsedDevice
    {
        public Guid Id { get; }
        private readonly DateTime _usedFrom;

        private UsedDevice(Guid id, DateTime usedFrom)
        {
            Id = id;
            _usedFrom = usedFrom;
        }

        public static UsedDevice UsedFrom(Guid id, DateTime from) => new UsedDevice(id, from);

        public override bool Equals(object obj)
            => obj is UsedDevice usedDevice && Id.Equals(usedDevice.Id);

        public override int GetHashCode() => HashCode.Combine(Id);
    }
}
