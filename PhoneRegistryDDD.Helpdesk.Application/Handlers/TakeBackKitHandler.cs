using System;
using System.Threading.Tasks;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Events;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using PhoneRegistryDDD.Shared.Abstractions.Commands;

namespace PhoneRegistryDDD.Helpdesk.Application.Handlers
{
    public class TakeBackKitHandler : ICommandHandler<TakeBackKitCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepo;

        public TakeBackKitHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }

        public async Task<bool> Handle(TakeBackKitCommand command)
        {
            Employee employee = await _employeeRepo.GetBy(command.EmployeeId) ?? Employee.New(Guid.NewGuid());
            Device deviceToReturn = new Device(command.DeviceId);

            ReturnedDevice result = employee.Return(deviceToReturn);
            if (result == null) return false;

            bool updateResult = await _employeeRepo.Update(employee);

            //TODO: Refactor to facade layer
            // if (updateResult)
            //     await _mediator.Publish(MapToCommand(result));

            return updateResult;
        }

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
}
