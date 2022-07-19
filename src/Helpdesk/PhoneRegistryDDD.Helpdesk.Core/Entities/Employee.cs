using System;
using System.Collections.Generic;
using System.Linq;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Events;

namespace PhoneRegistryDDD.Helpdesk.Core.Entities;

public class Employee
{
    private readonly List<SimCard> _simCards = new();

    public Guid Id { get; private set; }
    public IReadOnlyCollection<SimCard> SimCards => _simCards.AsReadOnly();
    private SimCard FreeSimCard => _simCards.Find(sim => sim.IsFree());

    [Obsolete("For EF", true)]
    public Employee()
    {
    }

    public Employee(Guid id, IEnumerable<SimCard> simCards)
    {
        Id = id;
        _simCards = simCards.ToList();
    }

    public static Employee New(Guid id) => new(id, new List<SimCard>());
    public static Employee With(Guid id, IEnumerable<SimCard> simCards) => new(id, simCards);

    public bool TakeNew(SimCard simCard)
    {
        if (HasFreeSimCard())
        {
            return false;
        }

        _simCards.Add(simCard);
        return true;
    }

    public bool HasFreeSimCard() => FreeSimCard != null;

    public bool TakeNew(Device device)
    {
        if (!HasFreeSimCard() || HasDeviceOfSameTypeAs(device))
        {
            return false;
        }

        FreeSimCard.SetDevice(device);
        return true;
    }

    public ReturnedDevice Return(Device device)
    {
        var simCard = GetSimCardWith(device);
        var doesntExists = simCard == null;

        if (doesntExists)
        {
            return null;
        }

        _simCards.Remove(simCard);
        var simCardSnapshot = simCard.ToSnapshot();

        return new ReturnedDevice(Id, simCardSnapshot.DeviceId, simCardSnapshot.Id);
    }

    private bool HasDeviceOfSameTypeAs(Device device)
        => _simCards.Any(sim => sim.HasDeviceOfSameTypeAs(device));

    private SimCard GetSimCardWith(Device device)
        => _simCards.Find(sim => sim.Has(device));
}