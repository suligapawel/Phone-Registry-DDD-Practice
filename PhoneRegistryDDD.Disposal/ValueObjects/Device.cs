using System;

namespace PhoneRegistryDDD.Utilization.ValueObjects
{
    public class Device
    {
        private readonly Guid _id;

        public Device(Guid id)
        {
            _id = id;
        }

        public override bool Equals(object obj)
            => obj is Device device && _id.Equals(device._id);


        public override int GetHashCode() => HashCode.Combine(_id);
    }
}
