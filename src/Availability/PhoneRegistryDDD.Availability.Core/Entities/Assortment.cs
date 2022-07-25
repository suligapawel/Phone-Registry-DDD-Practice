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

    private readonly List<Block> _blocks = new();
    public IReadOnlyCollection<Block> Blocks => _blocks.AsReadOnly();

    public Guid Id { get; init; }

    private Block TemporaryBlock => _blocks.FirstOrDefault();
    private Block PermanentBlock => _blocks.Skip(1).FirstOrDefault();

    [Obsolete("For EF", true)]
    public Assortment()
    {
    }

    private Assortment(Guid id, IReadOnlyCollection<Block> blocks)
    {
        if (blocks.Count > 2)
        {
            throw new ToManyBlockException();
        }

        Id = id;
        _blocks = blocks.ToList();
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
        _blocks.Remove(_blocks.First());
    }
}