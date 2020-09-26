using PhoneRegistryDDD.Utilization.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneRegistryDDD.Utilization.Entities
{
    public sealed class DeviceOwner
    {
        private readonly Guid _id;
        private readonly ICollection<Device> _currentlyUsed;
        private readonly ICollection<Device> _purchased;

        private DeviceOwner(Guid id, ICollection<Device> currentlyUsed, ICollection<Device> purchased)
        {
            _id = id;
            _currentlyUsed = currentlyUsed;
            _purchased = purchased;
        }

        public static DeviceOwner WithoutPurchasedHistory(Guid id, ICollection<Device> active)
            => new DeviceOwner(id, active, new List<Device>());

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
