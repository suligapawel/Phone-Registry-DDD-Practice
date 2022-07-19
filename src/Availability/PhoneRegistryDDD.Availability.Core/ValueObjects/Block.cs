using PhoneRegistryDDD.Availability.Core.Entities;

namespace PhoneRegistryDDD.Availability.Core.ValueObjects;

public sealed class Block
{
    private readonly Owner _owner;
    private bool _isPermanentBlock;

    private Block(Owner employee, bool isPermanentBlock)
    {
        _owner = employee;
        _isPermanentBlock = isPermanentBlock;
    }

    public static Block Temporary(Owner owner) => new(owner, false);
    public static Block Permanent(Owner owner) => new(owner, true);

    public bool CanBlockPermanently(Owner owner) => IsBlockedBySameOwner(owner);

    private bool IsBlockedBySameOwner(Owner owner) => _owner.Equals(owner);
}