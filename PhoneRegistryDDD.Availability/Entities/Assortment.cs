using PhoneRegistryDDD.Availability.Exceptions;
using PhoneRegistryDDD.Availability.ValueObjects;
using System;

namespace PhoneRegistryDDD.Availability.Entities
{
    public sealed class Assortment
    {
        private const int TEMPORARY_BLOCK_INDEX = 0;
        private const int PERMANENT_BLOCK_INDEX = 1;

        public Guid Id { get; }
        private readonly Block[] _blocks;

        private Block _temporaryBlock => _blocks[TEMPORARY_BLOCK_INDEX];
        private Block _permanentBlock => _blocks[PERMANENT_BLOCK_INDEX];

        private Assortment(Guid id, Block[] blocks)
        {
            if (blocks.Length > 2)
                throw new ToManyBlockException();

            Id = id;
            _blocks = new Block[2];
            blocks.CopyTo(_blocks, 0);
        }

        public static Assortment New() => new Assortment(Guid.NewGuid(), new Block[2]);
        public static Assortment FromSnapshot(Guid id, Block[] blocks) => new Assortment(id, blocks);

        public bool BlockTemporaryFor(Owner employee)
        {
            if (HasActiveBlocks())
                return false;

            _blocks[TEMPORARY_BLOCK_INDEX] = Block.Temporary(employee);
            return true;
        }

        public bool IsBlocked() => HasActiveBlocks();

        public bool Unblock()
        {
            if (HasPermanentBlock())
                return false;

            RemoveTemporaryBlock();

            return true;
        }

        private bool HasActiveBlocks() => HasTemporaryBlock() || HasPermanentBlock();
        private bool HasTemporaryBlock() => _temporaryBlock != null;
        private bool HasPermanentBlock() => _permanentBlock != null;

        private void RemoveTemporaryBlock()
        {
            _blocks[TEMPORARY_BLOCK_INDEX] = null;
        }

        public bool BlockPermanentlyFor(Owner owner)
        {
            if (HasPermanentBlock() || (!_temporaryBlock?.IsBlockedBySameOwner(owner) ?? false))
                return false;

            if (HasTemporaryBlock())
            {
                _blocks[PERMANENT_BLOCK_INDEX] = Block.Permanent(owner);
                return true;
            }

            return false;
        }
    }
}
