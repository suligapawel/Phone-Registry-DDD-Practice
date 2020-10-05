using System;

namespace PhoneRegistryDDD.Availability.Core.Entities
{
    public sealed class Owner
    {
        public Guid Id { get; }

        private Owner(Guid id)
        {
            Id = id;
        }

        public static Owner New(Guid guid) => new Owner(guid);
        public static Owner FromSnapshot(Guid id) => new Owner(id);

        public override bool Equals(object obj) => obj is Owner owner && owner.Id == Id;
        public override int GetHashCode() => Id.GetHashCode();
    }
}
