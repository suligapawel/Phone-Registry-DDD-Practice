﻿using System;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.ValueObjects;

namespace PhoneRegistryDDD.Helpdesk.Core.Entities;

public sealed class SimCard
{
    public Guid Id { get; private set; }
    public Device Device { get; private set; }

    [Obsolete("For EF", true)]
    public SimCard()
    {
    }

    private SimCard(Guid id)
    {
        Id = id;
    }

    private SimCard(Guid id, Device device)
        : this(id)
    {
        Device = device;
    }

    public static SimCard Free(Guid id) => new(id);
    public static SimCard With(Guid id, Device device) => new(id, device);
    public SimSnapshot ToSnapshot() => new(Id, Device.Id);

    internal bool IsFree() => Device == null;

    internal void SetDevice(Device device)
    {
        Device = device;
    }

    internal bool HasDeviceOfSameTypeAs(Device device)
    {
        if (IsFree())
        {
            return false;
        }

        return Device.IsSameTypeAs(device);
    }

    internal bool Has(Device device)
    {
        if (IsFree())
        {
            return false;
        }

        return Device.Equals(device);
    }
}