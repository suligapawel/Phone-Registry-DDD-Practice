using System;
using System.Collections.Generic;
using System.Linq;
using PhoneRegistryDDD.Helpdesk.Core.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Services.Devices.Policies;

namespace PhoneRegistryDDD.Helpdesk.Core.Services.Devices;

public class Choicer
{
    private readonly IEnumerable<DeviceToChoice> _availableDevices;

    public Choicer(IEnumerable<DeviceToChoice> availableDevices)
    {
        _availableDevices = availableDevices;
    }

    public Device Choice(IDeviceChoicer choicer)
    {
        ArgumentNullException.ThrowIfNull(choicer);

        var deviceClass = choicer.Choice();
        var deviceToChoice = FindBy(deviceClass);

        var notFound = deviceToChoice == null;
        return notFound ? null : MapFrom(deviceToChoice);
    }

    private DeviceToChoice FindBy(DeviceClass deviceClass)
        => _availableDevices.FirstOrDefault(device => device.Class == deviceClass);

    private static Device MapFrom(DeviceToChoice deviceToChoice)
        => Activator.CreateInstance(deviceToChoice.Type, deviceToChoice.Id) as Device;
}