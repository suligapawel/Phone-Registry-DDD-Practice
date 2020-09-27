using PhoneRegistryDDD.Disposal.Exceptions;
using PhoneRegistryDDD.Disposal.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneRegistryDDD.Disposal.Entities
{
    public sealed class DeviceOwner
    {
        private readonly Guid _id;
        private readonly ICollection<Device> _currentlyUsed;
        private readonly ICollection<Device> _purchased;

        private DeviceOwner(Guid id, ICollection<Device> currentlyUsed, ICollection<Device> purchased)
        {
            _id = id;

            if (UsesPurchasedDevice(currentlyUsed, purchased))
                throw new CannotUsePurchasedDeviceException();

            _currentlyUsed = currentlyUsed;
            _purchased = purchased;
        }

        public static DeviceOwner WithoutPurchasedHistory(Guid id, ICollection<Device> currentlyUsed)
            => new DeviceOwner(id, currentlyUsed, new List<Device>());

        public static DeviceOwner WithPurchasedHistory(Guid id, ICollection<Device> currentlyUsed, ICollection<Device> purchased)
            => new DeviceOwner(id, currentlyUsed, purchased);
        private static bool UsesPurchasedDevice(ICollection<Device> currentlyUsed, ICollection<Device> purchased)
             => currentlyUsed.Intersect(purchased).Any();

        public void Purchase(Device device)
        {
            if (NotUsed(device))
                return;

            _currentlyUsed.Remove(device);
            _purchased.Add(device);
        }

        public bool DidPurchase(Device device)
            => NotUsed(device) && Purchased(device);

        private bool Purchased(Device device)
            => _purchased.Any(x => x.Equals(device));

        private bool NotUsed(Device device)
            => !_currentlyUsed.Any(x => x.Equals(device));
    }
}
