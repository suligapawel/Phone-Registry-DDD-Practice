using System;
using System.Threading.Tasks;
using PhoneRegistryDDD.Helpdesk.Application.Events;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Events;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using PhoneRegistryDDD.Shared.Abstractions.Commands;

namespace PhoneRegistryDDD.Helpdesk.Application.Handlers
{
    public class TakeBackKitHandler : ICommandHandler<TakeBackKitCommand, KitReturned>
    {
        private readonly IEmployeeRepository _employeeRepo;

        public TakeBackKitHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }

        public async Task<KitReturned> Handle(TakeBackKitCommand command)
        {
            Employee employee = await _employeeRepo.GetBy(command.EmployeeId) ?? Employee.New(Guid.NewGuid());
            Device deviceToReturn = new Device(command.DeviceId);

            ReturnedDevice result = employee.Return(deviceToReturn);
            if (result == null) return null;

            bool updateResult = await _employeeRepo.Update(employee);

            //TODO: Refactor return without null
            return updateResult ? MapToEvent(result) : null;
        }

        private static KitReturned MapToEvent(ReturnedDevice returnedDevice)
            => new KitReturned(returnedDevice.DeviceId, returnedDevice.SimCardId);

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