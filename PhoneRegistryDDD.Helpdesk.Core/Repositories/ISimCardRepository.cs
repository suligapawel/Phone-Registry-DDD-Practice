using PhoneRegistryDDD.Helpdesk.Core.Entities;
using PhoneRegistryDDD.Helpdesk.Core.Entities.Devices;
using System;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Core.Repositories
{
    public interface ISimCardRepository
    {
        Task<SimCard> GetBy(Guid id);
        Task<bool> Update(SimCard simCard);
    }
}
