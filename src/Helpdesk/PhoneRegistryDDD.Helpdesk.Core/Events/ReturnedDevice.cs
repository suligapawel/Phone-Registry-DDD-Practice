using System;

namespace PhoneRegistryDDD.Helpdesk.Core.Events;

public class ReturnedDevice
{
    public Guid DeviceId { get; }
    public Guid SimCardId { get; }

    public ReturnedDevice(Guid employeeId, Guid deviceId, Guid simCardId)
    {
        DeviceId = deviceId;
        SimCardId = simCardId;
    }
}