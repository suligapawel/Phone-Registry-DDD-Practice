using System;
using SuligaPawel.Common.CQRS.Commands;

namespace PhoneRegistryDDD.Helpdesk.Core.Commands;

public class TakeBackKitCommand : ICommand
{
    public Guid EmployeeId { get; set; }
    public Guid DeviceId { get; set; }

    public TakeBackKitCommand()
    {
    }

    public TakeBackKitCommand(Guid employeeId, Guid deviceId)
    {
        EmployeeId = employeeId;
        DeviceId = deviceId;
    }
}