using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PhoneRegistryDDD.Helpdesk.Core.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Services.Devices;
using PhoneRegistryDDD.Helpdesk.Units.Services.DeviceChoicer.Fakes;

namespace PhoneRegistryDDD.Helpdesk.Units.Services.DeviceChoicer;

[TestFixture]
public class ChoicerTests
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
        var cache = AnyCache();
        var choicer = new Choicer(cache);

        var device = choicer.Choice(new PremiumChoicer());

        Assert.That(device.Id, Is.EqualTo(cache.First(x => x.Class == DeviceClass.Premium).Id));
    }

    [Test]
    public void When_FoundNewDevice_then_returnNothing()
    {
        var cache = AnyCache();
        var choicer = new Choicer(cache);

        var device = choicer.Choice(new StandardChoicer());

        Assert.That(device, Is.Null);
    }

    private IEnumerable<DeviceToChoice> AnyCache() => new List<DeviceToChoice>
    {
        new(_anyGuid, DeviceClass.Premium, typeof(Device)),
        new(_anyOtherGuid, DeviceClass.VIP, typeof(Device)),
    };
}