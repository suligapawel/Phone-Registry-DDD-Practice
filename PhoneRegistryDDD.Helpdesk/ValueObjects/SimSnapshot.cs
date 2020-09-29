using System;

namespace PhoneRegistryDDD.Helpdesk.ValueObjects
{
    public class SimSnapshot
    {
        internal Guid Id { get; }
        internal Guid DeviceId { get; }

        public SimSnapshot(Guid simId, Guid deviceId)
        {
            Id = simId;
            DeviceId = deviceId;
        }
    }
}
