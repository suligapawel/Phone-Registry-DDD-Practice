using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using System;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Core.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device> GetBy(Guid id);
        Task<bool> Update(Device device);
    }
}
