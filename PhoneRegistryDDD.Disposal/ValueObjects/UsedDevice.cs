using System;

namespace PhoneRegistryDDD.Disposal.ValueObjects
{
    public sealed class UsedDevice
    {
        public Guid Id { get; }
        private readonly DateTime _usedSince;

        private UsedDevice(Guid id, DateTime usedSince)
        {
            Id = id;
            _usedSince = usedSince;
        }

        public static UsedDevice UsedSince(Guid id, DateTime from) => new UsedDevice(id, from);

        public override bool Equals(object obj)
            => obj is UsedDevice usedDevice && Id.Equals(usedDevice.Id);

        public override int GetHashCode() => HashCode.Combine(Id);

        internal bool UsedTooShort(int monthLimit)
        {
            var now = DateTime.Now;
            var canPurchaseSince = _usedSince.AddMonths(monthLimit);

            return now < canPurchaseSince;
        }
    }
}
