using NUnit.Framework;
using PhoneRegistryDDD.Helpdesk.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Services.Devices.Policies;
using PhoneRegistryDDD.Helpdesk.ValueObjects;

namespace PhoneRegistryDDD.HelpdeskTests.Services.DeviceChoicer
{
    internal class SmartphoneChoicerTests
    {
        [Test]
        public void When_DepartmentAndPositionAreNotImportant_then_IsStandard()
        {
            EmployeeHrInfo hrInfo = new EmployeeHrInfo(AnyString(), AnyString());
            SmartphoneChoicer policy = new SmartphoneChoicer(hrInfo);

            DeviceClass result = policy.Choice();

            Assert.That(result, Is.EqualTo(DeviceClass.Standard));
        }

        [Test]
        public void When_DepartmentIsManagement_then_IsVIP()
        {
            EmployeeHrInfo hrInfo = new EmployeeHrInfo("Management", "Director");
            SmartphoneChoicer policy = new SmartphoneChoicer(hrInfo);

            DeviceClass result = policy.Choice();

            Assert.That(result, Is.EqualTo(DeviceClass.VIP));
        }

        [Test]
        public void When_PositionIsDirector_then_IsVIP()
        {
            EmployeeHrInfo hrInfo = new EmployeeHrInfo(AnyString(), "Director");
            SmartphoneChoicer policy = new SmartphoneChoicer(hrInfo);

            DeviceClass result = policy.Choice();

            Assert.That(result, Is.EqualTo(DeviceClass.VIP));
        }

        [Test]
        public void When_PositionIsManagement_then_IsPremium()
        {
            EmployeeHrInfo hrInfo = new EmployeeHrInfo(AnyString(), "Manager");
            SmartphoneChoicer policy = new SmartphoneChoicer(hrInfo);

            DeviceClass result = policy.Choice();

            Assert.That(result, Is.EqualTo(DeviceClass.Premium));
        }

        [Test]
        public void When_DepartmentIsIt_then_IsPremium()
        {
            EmployeeHrInfo hrInfo = new EmployeeHrInfo("IT", AnyString());
            SmartphoneChoicer policy = new SmartphoneChoicer(hrInfo);

            DeviceClass result = policy.Choice();

            Assert.That(result, Is.EqualTo(DeviceClass.Premium));
        }

        public string AnyString() => "whatever";
    }
}
