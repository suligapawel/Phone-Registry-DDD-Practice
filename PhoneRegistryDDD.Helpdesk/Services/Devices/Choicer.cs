using PhoneRegistryDDD.Helpdesk.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Services.Devices.Policies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneRegistryDDD.Helpdesk.Services.Devices
{
    public class Choicer
    {
        private readonly IEnumerable<DeviceToChoice> _availableDevices;

        public Choicer(IEnumerable<DeviceToChoice> availableDevices)
        {
            _availableDevices = availableDevices;
        }

        public Device Choice(IDeviceChoicer choicer)
        {
            DeviceClass deviceClass = choicer.Choice();
            DeviceToChoice deviceToChoice = FindBy(deviceClass);

            bool notFound = deviceToChoice == null;
            if (notFound)
                return null;

            return MapFrom(deviceToChoice);
        }

        private DeviceToChoice FindBy(DeviceClass deviceClass)
        {
            return _availableDevices.FirstOrDefault(device => device.Class == deviceClass);
        }

        private Device MapFrom(DeviceToChoice deviceToChoice)
            => Activator.CreateInstance(deviceToChoice.Type, deviceToChoice.Id) as Device;
    }
}
