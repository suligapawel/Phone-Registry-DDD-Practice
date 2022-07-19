using System;
using System.Collections.Generic;
using System.Linq;
using PhoneRegistryDDD.Disposal.Core.Exceptions;

namespace PhoneRegistryDDD.Disposal.Core.Entities;

public sealed class DeviceOwner
{
    private readonly Guid _id;
    private readonly ICollection<UsedDevice> _currentlyUsed;
    private readonly ICollection<PurchasedDevice> _purchased;

    private DeviceOwner(Guid id, ICollection<UsedDevice> currentlyUsed, ICollection<PurchasedDevice> purchased)
    {
        _id = id;

        if (UsesPurchasedDevice(currentlyUsed, purchased))
        {
            throw new CannotUsePurchasedDeviceException();
        }

        _currentlyUsed = currentlyUsed;
        _purchased = purchased;
    }

    public static DeviceOwner WithoutPurchasedHistory(Guid id, ICollection<UsedDevice> currentlyUsed) =>
        new(id, currentlyUsed, new List<PurchasedDevice>());

    public static DeviceOwner WithPurchasedHistory(Guid id, ICollection<UsedDevice> currentlyUsed, ICollection<PurchasedDevice> purchased) =>
        new(id, currentlyUsed, purchased);

    public void Purchase(PurchasedDevice device, int monthLimit)
    {
        if (NotUsed(device))
        {
            return;
        }

        var currentlyUsedDevice = GetUsedDeviceBy(device);

        if (currentlyUsedDevice.UsedTooShort(monthLimit))
        {
            return;
        }

        StopUsing(currentlyUsedDevice);
        Purchase(device);
    }

    public bool DidPurchase(PurchasedDevice device)
        => NotUsed(device) && Purchased(device);

    private static bool UsesPurchasedDevice(ICollection<UsedDevice> currentlyUsed, ICollection<PurchasedDevice> purchased)
    {
        foreach (var purchasedDevice in purchased)
        {
            var usePurchasedDevice = currentlyUsed.Any(usedDevice => purchasedDevice.IsSameDeviceAs(usedDevice));

            if (usePurchasedDevice)
            {
                return true;
            }
        }

        return false;
    }

    private UsedDevice GetUsedDeviceBy(PurchasedDevice device)
    {
        return _currentlyUsed.First(usedDevice => device.IsSameDeviceAs(usedDevice));
    }

    private void StopUsing(UsedDevice device)
    {
        _currentlyUsed.Remove(device);
    }

    private void Purchase(PurchasedDevice device)
    {
        _purchased.Add(device);
    }

    private bool Purchased(PurchasedDevice device)
        => _purchased.Any(x => x.Equals(device));

    private bool NotUsed(PurchasedDevice purchasedDevice)
        => !_currentlyUsed.Any(usedDevice => purchasedDevice.IsSameDeviceAs(usedDevice));
}