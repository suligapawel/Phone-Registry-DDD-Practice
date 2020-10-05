using System;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Services.Abstracts
{
    public interface IKitService
    {
        Task<bool> HandoverTo(Guid employeeId, Guid simCardId, Guid deviceId);
        Task<bool> TakeBackFrom(Guid userId, Guid deviceId);
    }
}
