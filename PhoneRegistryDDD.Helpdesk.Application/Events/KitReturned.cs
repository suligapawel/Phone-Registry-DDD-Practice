using System;

namespace PhoneRegistryDDD.Helpdesk.Application.Events
{
    public class KitReturned
    {
        public Guid DeviceId { get; }
        public Guid SimCardId { get; }

        public KitReturned(Guid deviceId, Guid simCardId)
        {
            DeviceId = deviceId;
            SimCardId = simCardId;
        }
    }
}
