using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Repositories
{
    //TODO
    public class DeviceRepository : IDeviceRepository
    {
        public async Task<Device> GetBy(Guid id) => new Smartphone(Guid.NewGuid());
        public async Task<bool> Update(Device device) => true;
    }
}
