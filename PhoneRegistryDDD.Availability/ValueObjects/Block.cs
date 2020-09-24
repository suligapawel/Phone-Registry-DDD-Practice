using PhoneRegistryDDD.Availability.Entities;
using System;

namespace PhoneRegistryDDD.Availability.ValueObjects
{
    public sealed class Block
    {
        private readonly Owner _owner;
        public bool _isPermanentBlock;
        private readonly DateTime _createdDate;

        private Block(Owner employee, bool isPermanentBlock, DateTime createdDate)
        {
            _owner = employee;
            _isPermanentBlock = isPermanentBlock;
            _createdDate = createdDate;
        }

        public static Block Temporary(Owner owner) => new Block(owner, false, DateTime.Now);
        public static Block Permanent(Owner owner) => new Block(owner, true, DateTime.Now);
        public static Block FromSnapshot(Owner owner, bool isPermanentBlock, DateTime createdDate)
            => new Block(owner, isPermanentBlock, createdDate);

        public bool CanBlockParmanently(Owner owner, int usingYears)
            => IsBlockedBySameOwner(owner) && IsTimeExceededToPermanentlyBlockade(usingYears);

        private bool IsBlockedBySameOwner(Owner owner) => _owner.Equals(owner);
        private bool IsTimeExceededToPermanentlyBlockade(int usingYears) => _createdDate <= DateTime.Now.AddYears(-usingYears);
    }
}
