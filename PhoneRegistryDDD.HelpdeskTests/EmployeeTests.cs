using NUnit.Framework;
using PhoneRegistryDDD.Helpdesk.Entities;
using PhoneRegistryDDD.Helpdesk.Entities.Devices;
using System.Collections.Generic;

namespace PhoneRegistryDDD.HelpdeskTests
{
    internal class EmployeeTests
    {
        [Test]
        public void When_EmployeeIsNew_then_TakeNewSimCard()
        {
            Employee employee = Employee.New();

            bool result = employee.TakeNew(NewSimCard());

            Assert.That(employee.HasFreeSimCard(), Is.True);
            Assert.That(result, Is.True);
        }

        [Test]
        public void When_EmployeeHasAllSimCardsOccupied_then_EmployeeCantTakeNewSimCard()
        {
            SimCard occupiedSimCard = SimCard.With(AnySmartphone());
            Employee employee = Employee.With(new List<SimCard> { occupiedSimCard });

            bool result = employee.TakeNew(NewSimCard());

            Assert.That(employee.HasFreeSimCard(), Is.True);
            Assert.That(result, Is.True);
        }

        [Test]
        public void When_EmployeeHasFreeFromDevicesSimCard_then_TakeNewSimCard()
        {
            SimCard freeSimCard = SimCard.Free();
            Employee employee = Employee.With(new List<SimCard> { freeSimCard });

            bool result = employee.TakeNew(NewSimCard());

            Assert.That(employee.HasFreeSimCard(), Is.True);
            Assert.That(result, Is.False);
        }

        [Test]
        public void When_EmployeeHasFreeSimCard_then_TakeNewDevice()
        {
            Device device = AnySmartphone();
            SimCard freeSimCard = SimCard.Free();
            Employee employee = Employee.With(new List<SimCard> { freeSimCard });

            bool result = employee.TakeNew(device);

            Assert.That(employee.HasFreeSimCard(), Is.False);
            Assert.That(result, Is.True);
        }

        [Test]
        public void When_EmployeeHasNotFreeSimCard_then_EmployeeCantTakeNewDevice()
        {
            Device device = AnySmartphone();
            SimCard occupiedSimCard = SimCard.With(AnySmartphone());
            Employee employee = Employee.With(new List<SimCard> { occupiedSimCard });

            bool result = employee.TakeNew(device);

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_EmployeeHasSameTypeOfDevice_then_EmployeeCantTakeNewDevice()
        {
            Device device = AnySmartphone();
            SimCard occupiedSimCard = SimCard.With(AnySmartphone());
            SimCard freeSimCard = SimCard.Free();
            Employee employee = Employee.With(new List<SimCard> { occupiedSimCard, freeSimCard });

            bool result = employee.TakeNew(device);

            Assert.That(result, Is.False);
        }

        private SimCard NewSimCard() => SimCard.Free();
        private Device AnySmartphone() => new Smartphone();
    }
}
