using System;
using PhoneRegistryDDD.Availability.Core.Entities;

namespace PhoneRegistryDDD.Availability.Core.ValueObjects;

public sealed class Block
{
    private bool _isPermanentBlock;
    
    public Owner Owner { get; init; }

    [Obsolete("For EF", true)]
    public Block()
    {
    }

    private Block(Owner employee, bool isPermanentBlock)
    {
        Owner = employee;
        _isPermanentBlock = isPermanentBlock;
    }

    public static Block Temporary(Owner owner) => new(owner, false);
    public static Block Permanent(Owner owner) => new(owner, true);

    public bool CanBlockPermanently(Owner owner) => IsBlockedBySameOwner(owner);

    private bool IsBlockedBySameOwner(Owner owner) => Owner.Equals(owner);
}