using PhoneRegistryDDD.Helpdesk.Core.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Core.ValueObjects;
using System;

namespace PhoneRegistryDDD.Helpdesk.Core.Services.Devices.Policies
{
    public class SmartphoneChoicer : IDeviceChoicer
    {
        private readonly EmployeeHrInfo _hrInfo;

        public SmartphoneChoicer(EmployeeHrInfo hrInfo)
        {
            _hrInfo = hrInfo;
        }

        public DeviceClass Choice()
        {
            if (WorkInManagement())
                return DeviceClass.VIP;

            if (IsResponsiblePerson())
                return DeviceClass.Premium;

            return DeviceClass.Standard;
        }

        private bool IsResponsiblePerson()
            => _hrInfo.Department.Contains("IT") || _hrInfo.Position.Contains("Manager");

        private bool WorkInManagement()
            => _hrInfo.Department.Contains("Management") || _hrInfo.Position.Contains("Director");
    }
}
