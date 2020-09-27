using NUnit.Framework;
using PhoneRegistryDDD.Disposal.Entities;
using PhoneRegistryDDD.Disposal.Exceptions;
using PhoneRegistryDDD.Disposal.ValueObjects;
using System;
using System.Collections.Generic;

namespace PhoneRegistryDDD.DisposalTests
{
    internal class DeviceOwnerTests
    {
        private UsedDevice _anyUsedDevice;
        private PurchasedDevice _samePurchasedDeviceAsTheUsedOne;
        private List<UsedDevice> _usedDevices;
        private List<PurchasedDevice> _purchasedDevices;

        [SetUp]
        public void SetUp()
        {
            var guid = Guid.NewGuid();
            _anyUsedDevice = UsedDevice.UsedFrom(guid, new DateTime(2018, 1, 1));
            _samePurchasedDeviceAsTheUsedOne = new PurchasedDevice(guid);
            _usedDevices = new List<UsedDevice> { _anyUsedDevice };
            _purchasedDevices = new List<PurchasedDevice> { _samePurchasedDeviceAsTheUsedOne };
        }

        [Test]
        public void When_OwnerPurchaseCurrentlyUsedDevice_then_PurchaseItOut()
        {
            DeviceOwner deviceOwner = DeviceOwner.WithoutPurchasedHistory(Guid.NewGuid(), _usedDevices);
            PurchasedDevice purchasedDevice = _samePurchasedDeviceAsTheUsedOne;

            deviceOwner.Purchase(purchasedDevice, 24);

            Assert.That(deviceOwner.DidPurchase(purchasedDevice), Is.True);
        }

        [Test]
        public void When_OwnerHasNotCurrentlyUsedDevice_then_CantPurchaseDevice()
        {
            DeviceOwner deviceOwner = DeviceOwner.WithoutPurchasedHistory(Guid.NewGuid(), new List<UsedDevice>());
            PurchasedDevice purchasedDevice = _samePurchasedDeviceAsTheUsedOne;

            deviceOwner.Purchase(purchasedDevice, 24);

            Assert.That(deviceOwner.DidPurchase(purchasedDevice), Is.False);
        }

        [Test]
        public void When_OwnerHasBothPurchasedAndUsedDevice_then_ThrowCannotUsePurchasedDeviceException()
        {
            Assert.Catch<CannotUsePurchasedDeviceException>(() => DeviceOwner.WithPurchasedHistory(Guid.NewGuid(), _usedDevices, _purchasedDevices));
        }
    }
}
