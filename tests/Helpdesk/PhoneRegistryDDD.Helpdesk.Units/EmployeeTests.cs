using System;
using System.Collections.Generic;
using NUnit.Framework;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;

namespace PhoneRegistryDDD.Helpdesk.Units;

[TestFixture]
public class EmployeeTests
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
        var employee = Employee.New(_anyEmployeeId);

        var result = employee.TakeNew(NewSimCard());

        Assert.That(employee.HasFreeSimCard(), Is.True);
        Assert.That(result, Is.True);
    }

    [Test]
    public void When_EmployeeHasAllSimCardsOccupied_then_EmployeeCantTakeNewSimCard()
    {
        var occupiedSimCard = SimCard.With(_anySimId, AnyDevice());
        var employee = Employee.With(Guid.NewGuid(), new List<SimCard> { occupiedSimCard });

        var result = employee.TakeNew(NewSimCard());

        Assert.That(employee.HasFreeSimCard(), Is.True);
        Assert.That(result, Is.True);
    }

    [Test]
    public void When_EmployeeHasFreeFromDevicesSimCard_then_TakeNewSimCard()
    {
        var freeSimCard = NewSimCard();
        var employee = Employee.With(Guid.NewGuid(), new List<SimCard> { freeSimCard });

        var result = employee.TakeNew(NewSimCard());

        Assert.That(employee.HasFreeSimCard(), Is.True);
        Assert.That(result, Is.False);
    }

    [Test]
    public void When_EmployeeHasFreeSimCard_then_TakeNewDevice()
    {
        var device = AnyDevice();
        var freeSimCard = NewSimCard();
        var employee = Employee.With(Guid.NewGuid(), new List<SimCard> { freeSimCard });

        var result = employee.TakeNew(device);

        Assert.That(employee.HasFreeSimCard(), Is.False);
        Assert.That(result, Is.True);
    }

    [Test]
    public void When_EmployeeHasNotFreeSimCard_then_EmployeeCantTakeNewDevice()
    {
        var device = AnyDevice();
        var occupiedSimCard = SimCard.With(_anySimId, AnyDevice());
        var employee = Employee.With(Guid.NewGuid(), new List<SimCard> { occupiedSimCard });

        var result = employee.TakeNew(device);

        Assert.That(result, Is.False);
    }

    [Test]
    public void When_EmployeeHasSameTypeOfDevice_then_EmployeeCantTakeNewDevice()
    {
        var device = AnyDevice();
        var occupiedSimCard = SimCard.With(_anySimId, AnyDevice());
        var freeSimCard = NewSimCard();
        var employee = Employee.With(Guid.NewGuid(), new List<SimCard> { occupiedSimCard, freeSimCard });

        var result = employee.TakeNew(device);

        Assert.That(result, Is.False);
    }

    [Test]
    public void When_EmployeeReturnDevice_thenReturnAlsoSimCard()
    {
        var anyDevice = AnyDevice();
        var occupiedSimCard = SimCard.With(_anySimId, anyDevice);
        var employee = Employee.With(_anyEmployeeId, new List<SimCard> { occupiedSimCard });

        var result = employee.Return(anyDevice);

        Assert.That(employee.HasFreeSimCard(), Is.False);
    }

    [Test]
    public void When_EmployeeTryReturnDeviceThatDoesNotExists_then_EmployeeCantReturnsDevice()
    {
        var anyDevice = AnyDevice();
        var freeSimCard = SimCard.Free(_anySimId);
        var employee = Employee.With(_anyEmployeeId, new List<SimCard> { freeSimCard });

        var result = employee.Return(anyDevice);

        Assert.That(employee.HasFreeSimCard(), Is.True);
        Assert.That(result, Is.Null);
    }

    private SimCard NewSimCard() => SimCard.Free(_anySimId);
    private Device AnyDevice() => new(_anyDeviceId);
}