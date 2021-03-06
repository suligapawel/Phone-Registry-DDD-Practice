﻿using NUnit.Framework;
using PhoneRegistryDDD.Helpdesk.Core.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Services.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Services.Devices.Policies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneRegistryDDD.HelpdeskTests.Services.DeviceChoicer
{
    internal class ChoicerTests
    {
        private Guid _anyGuid;
        private Guid _anyOtherGuid;

        [SetUp]
        public void Setup()
        {
            _anyGuid = Guid.NewGuid();
            _anyOtherGuid = Guid.NewGuid();
        }

        [Test]
        public void When_FoundNewDevice_then_returnThisDevice()
        {
            IEnumerable<DeviceToChoice> cache = AnyCache();
            Choicer choicer = new Choicer(cache);

            Device device = choicer.Choice(new PremiumChoicer());

            Assert.That(device.Id, Is.EqualTo(cache.First(x => x.Class == DeviceClass.Premium).Id));
        }

        [Test]
        public void When_FoundNewDevice_then_returnNothing()
        {
            IEnumerable<DeviceToChoice> cache = AnyCache();
            Choicer choicer = new Choicer(cache);

            Device device = choicer.Choice(new StandardChoicer());

            Assert.That(device, Is.Null);
        }

        private IEnumerable<DeviceToChoice> AnyCache() => new List<DeviceToChoice>
        {
            new DeviceToChoice(_anyGuid, DeviceClass.Premium, typeof(Device)),
            new DeviceToChoice(_anyOtherGuid, DeviceClass.VIP, typeof(Device)),
        };
    }

    internal class PremiumChoicer : IDeviceChoicer
    {
        public DeviceClass Choice() => DeviceClass.Premium;
    }

    internal class StandardChoicer : IDeviceChoicer
    {
        public DeviceClass Choice() => DeviceClass.Standard;
    }
}
