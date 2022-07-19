using System;

namespace PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;

public class Device
{
    public Guid Id { get; private set; }

    [Obsolete("For EF", true)]
    public Device()
    {
    }

    public Device(Guid id)
    {
        Id = id;
    }

    public override bool Equals(object obj) =>
        obj is Device device &&
        Id.Equals(device.Id);

    public override int GetHashCode() => HashCode.Combine(Id);

    internal bool IsSameTypeAs(Device device) => GetType() == device.GetType();
}