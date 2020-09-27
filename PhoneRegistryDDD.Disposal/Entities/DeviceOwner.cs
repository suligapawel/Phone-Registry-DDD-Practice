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
        private readonly ICollection<UsedDevice> _currentlyUsed;
        private readonly ICollection<PurchasedDevice> _purchased;

        private DeviceOwner(Guid id, ICollection<UsedDevice> currentlyUsed, ICollection<PurchasedDevice> purchased)
        {
            _id = id;

            if (UsesPurchasedDevice(currentlyUsed, purchased))
                throw new CannotUsePurchasedDeviceException();

            _currentlyUsed = currentlyUsed;
            _purchased = purchased;
        }

        public static DeviceOwner WithoutPurchasedHistory(Guid id, ICollection<UsedDevice> currentlyUsed)
            => new DeviceOwner(id, currentlyUsed, new List<PurchasedDevice>());

        public static DeviceOwner WithPurchasedHistory(Guid id, ICollection<UsedDevice> currentlyUsed, ICollection<PurchasedDevice> purchased)
            => new DeviceOwner(id, currentlyUsed, purchased);

        private static bool UsesPurchasedDevice(ICollection<UsedDevice> currentlyUsed, ICollection<PurchasedDevice> purchased)
        {
            foreach (var purchasedDevice in purchased)
            {
                bool usePurchasedDevice = currentlyUsed.Any(usedDevice => purchasedDevice.IsSameDeviceAs(usedDevice));

                if (usePurchasedDevice)
                    return true;
            }

            return false;
        }

        //TODO: Używany przez określony czas
        public void Purchase(PurchasedDevice device, int usedMonth)
        {
            if (NotUsed(device))
                return;

            StopUsing(device);
            Purchase(device);
        }

        private void StopUsing(PurchasedDevice purchasedDevice)
        {
            var currentlyUsedDevice = _currentlyUsed.First(usedDevice => purchasedDevice.IsSameDeviceAs(usedDevice));

            _currentlyUsed.Remove(currentlyUsedDevice);
        }

        private void Purchase(PurchasedDevice device)
        {
            _purchased.Add(device);
        }

        public bool DidPurchase(PurchasedDevice device)
            => NotUsed(device) && Purchased(device);

        private bool Purchased(PurchasedDevice device)
            => _purchased.Any(x => x.Equals(device));

        private bool NotUsed(PurchasedDevice purchasedDevice)
            => !_currentlyUsed.Any(usedDevice => purchasedDevice.IsSameDeviceAs(usedDevice));
    }
}
