﻿using PhoneRegistryDDD.Availability.Core.Exceptions;
using PhoneRegistryDDD.Availability.Core.ValueObjects;
using System;

namespace PhoneRegistryDDD.Availability.Core.Entities
{
    public sealed class Assortment
    {
        private const int TEMPORARY_BLOCK_INDEX = 0;
        private const int PERMANENT_BLOCK_INDEX = 1;

        private readonly Guid _id;
        private readonly Block[] _blocks;

        private Block _temporaryBlock => _blocks[TEMPORARY_BLOCK_INDEX];
        private Block _permanentBlock => _blocks[PERMANENT_BLOCK_INDEX];

        private Assortment(Guid id, Block[] blocks)
        {
            if (blocks.Length > 2)
                throw new ToManyBlockException();

            _id = id;
            _blocks = new Block[2];
            blocks.CopyTo(_blocks, 0);
        }

        public static Assortment New() => new Assortment(Guid.NewGuid(), new Block[2]);
        public static Assortment FromSnapshot(Guid id, Block[] blocks) => new Assortment(id, blocks);

        public bool BlockTemporaryFor(Owner owner)
        {
            if (HasActiveBlocks())
                return false;

            _blocks[TEMPORARY_BLOCK_INDEX] = Block.Temporary(owner);
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
            if (HasPermanentBlock() || (HasTemporaryBlock() && !_temporaryBlock.CanBlockParmanently(owner)))
                return false;

            _blocks[PERMANENT_BLOCK_INDEX] = Block.Permanent(owner);
            return true;
        }
    }
}
