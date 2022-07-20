using System;
using System.Threading;
using System.Threading.Tasks;
using PhoneRegistryDDD.Helpdesk.Application.Events;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Events;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using SuligaPawel.Common.CQRS.Commands;

namespace PhoneRegistryDDD.Helpdesk.Application.Handlers;

public class TakeBackKitHandler : ICommandHandler<TakeBackKitCommand>
{
    private readonly IEmployeeRepository _employeeRepo;

    public TakeBackKitHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepo = employeeRepository;
    }

    public async Task Handle(TakeBackKitCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var employee = await _employeeRepo.GetBy(command.EmployeeId) ?? Employee.New(Guid.NewGuid());
        var deviceToReturn = new Device(command.DeviceId);

        var result = employee.Return(deviceToReturn);
        if (result == null)
        {
            return;
        }

        var updateResult = await _employeeRepo.Update(employee);

        // TODO: publish event
    }

    private static KitReturned MapToEvent(ReturnedDevice returnedDevice) => new(returnedDevice.DeviceId, returnedDevice.SimCardId);

    // private UnblockAssortmentCommand MapToCommand(ReturnedDevice result)
    // {
    //     var assortmentIds = new Guid[]
    //     {
    //         result.DeviceId,
    //         result.SimCardId
    //     };
    //
    //     return new UnblockAssortmentCommand(assortmentIds);
    // }
}