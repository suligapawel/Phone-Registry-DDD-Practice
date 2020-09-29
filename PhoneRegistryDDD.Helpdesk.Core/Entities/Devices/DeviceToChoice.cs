using PhoneRegistryDDD.Helpdesk.Core.Dictionaries;
using System;

namespace PhoneRegistryDDD.Helpdesk.Core.Entities.Devices
{
    public class DeviceToChoice
    {
        public Guid Id { get; }
        public DeviceClass Class { get; }
        public Type Type { get; set; }

        public DeviceToChoice(Guid id, DeviceClass @class, Type type)
        {
            Id = id;
            Class = @class;
            Type = type;
        }

        public override bool Equals(object obj)
        {
            return obj is DeviceToChoice choice &&
                   Id.Equals(choice.Id) &&
                   Class == choice.Class;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Class);
        }
    }
}
