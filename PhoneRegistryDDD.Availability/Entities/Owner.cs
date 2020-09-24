using System;

namespace PhoneRegistryDDD.Availability.Entities
{
    public sealed class Owner
    {
        public Guid Id { get; }

        private Owner(Guid id)
        {
            Id = id;
        }

        public static Owner New() => new Owner(Guid.NewGuid());
        public static Owner FromSnapshot(Guid id) => new Owner(id);

        public override bool Equals(object obj) => (obj is Owner owner) && owner.Id == Id;
        public override int GetHashCode() => Id.GetHashCode();
    }
}
