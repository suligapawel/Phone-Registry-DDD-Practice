using System;

namespace PhoneRegistryDDD.Availability.Core.Entities;

public sealed class Owner
{
    public Guid Id { get; init; }

    [Obsolete("For EF", true)]
    public Owner()
    {
    }

    private Owner(Guid id)
    {
        Id = id;
    }

    public static Owner New(Guid id) => new(id);
    public static Owner FromSnapshot(Guid id) => new(id);

    public override bool Equals(object obj) => obj is Owner owner && owner.Id == Id;
    public override int GetHashCode() => Id.GetHashCode();
}