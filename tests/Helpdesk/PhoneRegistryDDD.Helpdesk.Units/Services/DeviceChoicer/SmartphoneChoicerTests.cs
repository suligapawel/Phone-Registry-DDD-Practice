using NUnit.Framework;
using PhoneRegistryDDD.Helpdesk.Core.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Core.Services.Devices.Policies;
using PhoneRegistryDDD.Helpdesk.Core.ValueObjects;

namespace PhoneRegistryDDD.Helpdesk.Units.Services.DeviceChoicer;

[TestFixture]
public class SmartphoneChoicerTests
{
    [Test]
    public void When_DepartmentAndPositionAreNotImportant_then_IsStandard()
    {
        var hrInfo = new EmployeeHrInfo(AnyString(), AnyString());
        var policy = new SmartphoneChoicer(hrInfo);

        var result = policy.Choice();

        Assert.That(result, Is.EqualTo(DeviceClass.Standard));
    }

    [Test]
    public void When_DepartmentIsManagement_then_IsVIP()
    {
        var hrInfo = new EmployeeHrInfo("Management", "Director");
        var policy = new SmartphoneChoicer(hrInfo);

        var result = policy.Choice();

        Assert.That(result, Is.EqualTo(DeviceClass.VIP));
    }

    [Test]
    public void When_PositionIsDirector_then_IsVIP()
    {
        var hrInfo = new EmployeeHrInfo(AnyString(), "Director");
        var policy = new SmartphoneChoicer(hrInfo);

        var result = policy.Choice();

        Assert.That(result, Is.EqualTo(DeviceClass.VIP));
    }

    [Test]
    public void When_PositionIsManagement_then_IsPremium()
    {
        var hrInfo = new EmployeeHrInfo(AnyString(), "Manager");
        var policy = new SmartphoneChoicer(hrInfo);

        var result = policy.Choice();

        Assert.That(result, Is.EqualTo(DeviceClass.Premium));
    }

    [Test]
    public void When_DepartmentIsIt_then_IsPremium()
    {
        var hrInfo = new EmployeeHrInfo("IT", AnyString());
        var policy = new SmartphoneChoicer(hrInfo);

        var result = policy.Choice();

        Assert.That(result, Is.EqualTo(DeviceClass.Premium));
    }

    private static string AnyString() => "whatever";
}