﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using PhoneRegistryDDD.Disposal.Core.Entities;
using PhoneRegistryDDD.Disposal.Core.Exceptions;

namespace PhoneRegistryDDD.Disposal.Units;

[TestFixture]
public class DeviceOwnerTests
{
    private UsedDevice _anyUsedDevice;
    private PurchasedDevice _samePurchasedDeviceAsTheUsedOne;
    private List<UsedDevice> _usedDevices;
    private List<PurchasedDevice> _purchasedDevices;

    [SetUp]
    public void SetUp()
    {
        var guid = Guid.NewGuid();
        _anyUsedDevice = UsedDevice.UsedSince(guid, new DateTime(2018, 1, 1));
        _samePurchasedDeviceAsTheUsedOne = new PurchasedDevice(guid);
        _usedDevices = new List<UsedDevice> { _anyUsedDevice };
        _purchasedDevices = new List<PurchasedDevice> { _samePurchasedDeviceAsTheUsedOne };
    }

    [Test]
    public void When_OwnerPurchaseCurrentlyUsedDevice_then_PurchaseItOut()
    {
        var deviceOwner = DeviceOwner.WithoutPurchasedHistory(Guid.NewGuid(), _usedDevices);
        var purchasedDevice = _samePurchasedDeviceAsTheUsedOne;

        deviceOwner.Purchase(purchasedDevice, 0);

        Assert.That(deviceOwner.DidPurchase(purchasedDevice), Is.True);
    }

    [Test]
    public void When_OwnerHasNotCurrentlyUsedDevice_then_CantPurchaseDevice()
    {
        var deviceOwner = DeviceOwner.WithoutPurchasedHistory(Guid.NewGuid(), new List<UsedDevice>());
        var purchasedDevice = _samePurchasedDeviceAsTheUsedOne;

        deviceOwner.Purchase(purchasedDevice, 0);

        Assert.That(deviceOwner.DidPurchase(purchasedDevice), Is.False);
    }

    [Test]
    public void When_OwnerHasBothPurchasedAndUsedDevice_then_ThrowCannotUsePurchasedDeviceException()
    {
        Assert.Catch<CannotUsePurchasedDeviceException>(() =>
            DeviceOwner.WithPurchasedHistory(Guid.NewGuid(), _usedDevices, _purchasedDevices));
    }

    [Test]
    public void When_OwnerHasNotUsedDeviceForCertainPeriodOfTime_then_CantPurchaseDevice()
    {
        var deviceOwner = DeviceOwner.WithoutPurchasedHistory(Guid.NewGuid(), _usedDevices);
        var purchasedDevice = _samePurchasedDeviceAsTheUsedOne;

        deviceOwner.Purchase(purchasedDevice, 1000);

        Assert.That(deviceOwner.DidPurchase(purchasedDevice), Is.False);
    }
}