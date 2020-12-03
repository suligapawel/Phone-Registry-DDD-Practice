using NUnit.Framework;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Events;
using System;
using System.Collections.Generic;

namespace PhoneRegistryDDD.HelpdeskTests
{
    internal class EmployeeTests
    {
        private Guid _anyEmployeeId;
        private Guid _anySimId;
        private Guid _anyDeviceId;

        [SetUp]
        public void SetUp()
        {
            _anyEmployeeId = Guid.NewGuid();
            _anySimId = Guid.NewGuid();
            _anyDeviceId = Guid.NewGuid();
        }

        [Test]
        public void When_EmployeeIsNew_then_TakeNewSimCard()
        {
            Employee employee = Employee.New(_anyEmployeeId);

            bool result = employee.TakeNew(NewSimCard());

            Assert.That(employee.HasFreeSimCard(), Is.True);
            Assert.That(result, Is.True);
        }

        [Test]
        public void When_EmployeeHasAllSimCardsOccupied_then_EmployeeCantTakeNewSimCard()
        {
            SimCard occupiedSimCard = SimCard.With(_anySimId, AnyDevice());
            Employee employee = Employee.With(Guid.NewGuid(), new List<SimCard> { occupiedSimCard });

            bool result = employee.TakeNew(NewSimCard());

            Assert.That(employee.HasFreeSimCard(), Is.True);
            Assert.That(result, Is.True);
        }

        [Test]
        public void When_EmployeeHasFreeFromDevicesSimCard_then_TakeNewSimCard()
        {
            SimCard freeSimCard = NewSimCard();
            Employee employee = Employee.With(Guid.NewGuid(), new List<SimCard> { freeSimCard });

            bool result = employee.TakeNew(NewSimCard());

            Assert.That(employee.HasFreeSimCard(), Is.True);
            Assert.That(result, Is.False);
        }

        [Test]
        public void When_EmployeeHasFreeSimCard_then_TakeNewDevice()
        {
            Device device = AnyDevice();
            SimCard freeSimCard = NewSimCard();
            Employee employee = Employee.With(Guid.NewGuid(), new List<SimCard> { freeSimCard });

            bool result = employee.TakeNew(device);

            Assert.That(employee.HasFreeSimCard(), Is.False);
            Assert.That(result, Is.True);
        }

        [Test]
        public void When_EmployeeHasNotFreeSimCard_then_EmployeeCantTakeNewDevice()
        {
            Device device = AnyDevice();
            SimCard occupiedSimCard = SimCard.With(_anySimId, AnyDevice());
            Employee employee = Employee.With(Guid.NewGuid(), new List<SimCard> { occupiedSimCard });

            bool result = employee.TakeNew(device);

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_EmployeeHasSameTypeOfDevice_then_EmployeeCantTakeNewDevice()
        {
            Device device = AnyDevice();
            SimCard occupiedSimCard = SimCard.With(_anySimId, AnyDevice());
            SimCard freeSimCard = NewSimCard();
            Employee employee = Employee.With(Guid.NewGuid(), new List<SimCard> { occupiedSimCard, freeSimCard });

            bool result = employee.TakeNew(device);

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_EmployeeReturnDevice_thenReturnAlsoSimCard()
        {
            Device anyDevice = AnyDevice();
            SimCard occupiedSimCard = SimCard.With(_anySimId, anyDevice);
            Employee employee = Employee.With(_anyEmployeeId, new List<SimCard> { occupiedSimCard });

            ReturnedDevice result = employee.Return(anyDevice);

            Assert.That(employee.HasFreeSimCard(), Is.False);
        }

        [Test]
        public void When_EmployeeTryReturnDeviceThatDoesNotExists_then_EmployeeCantReturnsDevice()
        {
            Device anyDevice = AnyDevice();
            SimCard freeSimCard = SimCard.Free(_anySimId);
            Employee employee = Employee.With(_anyEmployeeId, new List<SimCard> { freeSimCard });

            ReturnedDevice result = employee.Return(anyDevice);

            Assert.That(employee.HasFreeSimCard(), Is.True);
            Assert.That(result, Is.Null);
        }

        private SimCard NewSimCard() => SimCard.Free(_anySimId);
        private Device AnyDevice() => new Device(_anyDeviceId);
    }
}
