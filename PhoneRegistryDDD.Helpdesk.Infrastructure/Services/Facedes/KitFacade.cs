using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;
using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Services.Facedes
{
    public class KitFacade
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISimCardRepository _simCardRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IAssortmentRepository _assortmentRepository;

        public KitFacade(IEmployeeRepository employeeRepository,
            ISimCardRepository simCardRepository,
            IDeviceRepository deviceRepository,
            IAssortmentRepository assortmentRepository)
        {
            _employeeRepository = employeeRepository;
            _simCardRepository = simCardRepository;
            _deviceRepository = deviceRepository;
            _assortmentRepository = assortmentRepository;
        }

        public Task<IEnumerable<Assortment>> GetAssortmentsBy(params Guid[] ids) => _assortmentRepository.GetFewBy(ids);
        public Task<bool> UpdateAssortments(IEnumerable<Assortment> assortment) => _assortmentRepository.UpdateFew(assortment);
        public Task<Employee> GetEmployeeBy(Guid id) => _employeeRepository.GetBy(id);
        public Task<bool> UpdateEmployee(Employee employee) => _employeeRepository.Update(employee);
        public Task<SimCard> GetSimCardBy(Guid id) => _simCardRepository.GetBy(id);
        public Task<Device> GetDeviceBy(Guid id) => _deviceRepository.GetBy(id);
    }
}
