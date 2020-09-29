using NUnit.Framework;
using PhoneRegistryDDD.Helpdesk.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Services.Devices;
using PhoneRegistryDDD.Helpdesk.Services.Devices.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            new DeviceToChoice(_anyGuid, DeviceClass.Premium, typeof(Smartphone)),
            new DeviceToChoice(_anyOtherGuid, DeviceClass.VIP, typeof(Smartphone)),
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
