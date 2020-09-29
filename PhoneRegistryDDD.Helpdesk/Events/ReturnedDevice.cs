using System;

namespace PhoneRegistryDDD.Helpdesk.Events
{
    public class ReturnedDevice
    {
        public DateTime When { get; }
        public Guid EmployeeId { get; }
        public Guid DeviceId { get; }
        public Guid SimCardId { get; }

        public ReturnedDevice(Guid employeeId, Guid deviceId, Guid simCardId)
        {
            When = DateTime.Now;
            EmployeeId = employeeId;
            DeviceId = deviceId;
            SimCardId = simCardId;
        }
    }
}
