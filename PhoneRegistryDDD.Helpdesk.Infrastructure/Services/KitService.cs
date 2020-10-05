using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Events;
using PhoneRegistryDDD.Helpdesk.Infrastructure.Services.Abstracts;
using PhoneRegistryDDD.Helpdesk.Infrastructure.Services.Facedes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Services
{
    public class KitService : IKitService
    {
        private readonly KitFacade _facade;

        public KitService(KitFacade facade)
        {
            _facade = facade;
        }

        public async Task<bool> HandoverTo(Guid employeeId, Guid simCardId, Guid deviceId)
        {
            bool handOverResult = await HandoverKitTo(employeeId, simCardId, deviceId);

            if (!handOverResult)
                return false;

            return await BlockAssortments(employeeId, simCardId, deviceId);
        }

        private async Task<bool> HandoverKitTo(Guid employeeId, Guid simCardId, Guid deviceId)
        {
            Employee employee = await _facade.GetEmployeeBy(employeeId);
            SimCard simCard = await _facade.GetSimCardBy(simCardId);
            Device device = await _facade.GetDeviceBy(deviceId);

            bool handoverSimCardResult = employee.TakeNew(simCard);
            bool handoverDeviceResult = employee.TakeNew(device);

            if (!handoverSimCardResult || handoverDeviceResult)
                return false;

            return await _facade.UpdateEmployee(employee);
        }

        private async Task<bool> BlockAssortments(Guid ownerId, Guid simCardId, Guid deviceId)
        {
            var owner = Owner.New(ownerId);
            IEnumerable<Assortment> assortments = await _facade.GetAssortmentsBy(simCardId, deviceId);

            foreach (Assortment assortment in assortments)
            {
                bool blockResult = assortment.BlockTemporaryFor(owner);
                if (!blockResult)
                    return false;
            }

            return await _facade.UpdateAssortments(assortments);
        }

        public async Task<bool> TakeBackFrom(Guid employeeId, Guid deviceId)
        {
            Employee employee = await _facade.GetEmployeeBy(employeeId);
            Device deviceToReturn = await _facade.GetDeviceBy(deviceId);

            ReturnedDevice result = employee.Return(deviceToReturn);

            bool updateResult = await _facade.UpdateEmployee(employee);
            if (!updateResult)
                return false;

            return await UnblockAssortments(result);
        }

        private async Task<bool> UnblockAssortments(ReturnedDevice result)
        {
            IEnumerable<Assortment> assortments = await _facade.GetAssortmentsBy(result.SimCardId, result.DeviceId);

            foreach (var assortment in assortments)
            {
                bool unblockResult = assortment.Unblock();
                if (unblockResult)
                    return false;
            }

            return await _facade.UpdateAssortments(assortments);
        }
    }
}
