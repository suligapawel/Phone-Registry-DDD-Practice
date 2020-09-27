using NUnit.Framework;
using PhoneRegistryDDD.Utilization.Entities;
using PhoneRegistryDDD.Utilization.Exceptions;
using PhoneRegistryDDD.Utilization.ValueObjects;
using System;
using System.Collections.Generic;

namespace PhoneRegistryDDD.UtilizationTests
{
    internal class DeviceOwnerTests
    {
        private Device _anyDevice;
        private List<Device> _deviceCollection;

        [SetUp]
        public void SetUp()
        {
            var guid = Guid.NewGuid();
            _anyDevice = new Device(guid);
            _deviceCollection = new List<Device> { _anyDevice };
        }

        [Test]
        public void When_OwnerPurchaseCurrentlyUsedDevice_then_PurchaseItOut()
        {
            DeviceOwner deviceOwner = DeviceOwner.WithoutPurchasedHistory(Guid.NewGuid(), _deviceCollection);
            Device device = _anyDevice;

            deviceOwner.Purchase(device);

            Assert.That(deviceOwner.DidPurchase(device), Is.True);
        }

        [Test]
        public void When_OwnerHasNotCurrentlyUsedDevice_then_CantPurchaseDevice()
        {
            DeviceOwner deviceOwner = DeviceOwner.WithoutPurchasedHistory(Guid.NewGuid(), new List<Device>());
            Device device = _anyDevice;

            deviceOwner.Purchase(device);

            Assert.That(deviceOwner.DidPurchase(device), Is.False);
        }

        [Test]
        public void When_OwnerHasBothPurchasedAndUsedDevice_then_ThrowCannotUsePurchasedDeviceException()
        {
            Assert.Catch<CannotUsePurchasedDeviceException>(() => DeviceOwner.WithPurchasedHistory(Guid.NewGuid(), _deviceCollection, _deviceCollection));
        }
    }
}
