using MediatR;
using System;

namespace PhoneRegistryDDD.Helpdesk.Core.Commands
{
    public class TakeBackKitCommand : IRequest<bool>
    {
        public Guid EmployeeId { get; set; }
        public Guid DeviceId { get; set; }

        public TakeBackKitCommand() { }

        public TakeBackKitCommand(Guid employeeId, Guid deviceId)
        {
            EmployeeId = employeeId;
            DeviceId = deviceId;
        }
    }
}
