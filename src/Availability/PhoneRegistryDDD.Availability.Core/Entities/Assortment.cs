using System;
using System.Collections.Generic;
using System.Linq;
using PhoneRegistryDDD.Availability.Core.Exceptions;
using PhoneRegistryDDD.Availability.Core.ValueObjects;

namespace PhoneRegistryDDD.Availability.Core.Entities;

public sealed class Assortment
{
    private const int TemporaryBlockIndex = 0;
    private const int PermanentBlockIndex = 1;

    private readonly Guid _id;
    private readonly Block[] _blocks;

    private Block TemporaryBlock => _blocks[TemporaryBlockIndex];
    private Block PermanentBlock => _blocks[PermanentBlockIndex];

    private Assortment(Guid id, Block[] blocks)
    {
        if (blocks.Length > 2)
        {
            throw new ToManyBlockException();
        }

        _id = id;
        _blocks = new Block[2];
        blocks.CopyTo(_blocks, 0);
    }

    public static Assortment New() => new(Guid.NewGuid(), new Block[2]);
    public static Assortment FromSnapshot(Guid id, IEnumerable<Block> blocks) => new(id, blocks.ToArray());

    public bool BlockTemporaryFor(Owner owner)
    {
        if (HasActiveBlocks())
        {
            return false;
        }

        _blocks[TemporaryBlockIndex] = Block.Temporary(owner);
        return true;
    }

    public bool IsBlocked() => HasActiveBlocks();

    public bool Unblock()
    {
        if (HasPermanentBlock())
        {
            return false;
        }

        RemoveTemporaryBlock();

        return true;
    }

    public bool BlockPermanentlyFor(Owner owner)
    {
        if (HasPermanentBlock() || (HasTemporaryBlock() && !TemporaryBlock.CanBlockPermanently(owner)))
        {
            return false;
        }

        _blocks[PermanentBlockIndex] = Block.Permanent(owner);
        return true;
    }

    private bool HasActiveBlocks() => HasTemporaryBlock() || HasPermanentBlock();
    private bool HasTemporaryBlock() => TemporaryBlock != null;
    private bool HasPermanentBlock() => PermanentBlock != null;

    private void RemoveTemporaryBlock()
    {
        _blocks[TemporaryBlockIndex] = null;
    }
}