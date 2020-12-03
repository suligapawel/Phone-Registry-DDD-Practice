using MediatR;
using PhoneRegistryDDD.Availability.Core.Commands;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Events;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Handlers
{
    public class TakeBackKitHandler : IRequestHandler<TakeBackKitCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepo;

        public TakeBackKitHandler(IMediator mediator, IEmployeeRepository employeeRepository)
        {
            _mediator = mediator;
            _employeeRepo = employeeRepository;
        }

        public async Task<bool> Handle(TakeBackKitCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _employeeRepo.GetBy(request.EmployeeId) ?? Employee.New(Guid.NewGuid());
            Device deviceToReturn = new Device(request.DeviceId);

            ReturnedDevice result = employee.Return(deviceToReturn);
            if (result == null) return false;

            bool updateResult = await _employeeRepo.Update(employee);

            if (updateResult)
                await _mediator.Publish(MapToCommand(result));

            return updateResult;
        }

        private UnblockAssortmentCommand MapToCommand(ReturnedDevice result)
        {
            var assortmentIds = new Guid[]
            {
                result.DeviceId,
                result.SimCardId
            };

            return new UnblockAssortmentCommand(assortmentIds);
        }
    }
}
