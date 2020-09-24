using NUnit.Framework;
using PhoneRegistryDDD.Availability.Entities;
using PhoneRegistryDDD.Availability.ValueObjects;
using System;

namespace PhoneRegistryDDD.AvailabilityTests
{
    public class AssortmentTests
    {
        private Guid _anyOwnerGuid;

        [SetUp]
        public void Setup()
        {
            _anyOwnerGuid = Guid.NewGuid();
        }

        [Test]
        public void When_assortmentHasNotBlocks_then_blockAssortment()
        {
            Assortment assortment = NotBlockedAssortment();
            Owner owner = OwnerFromSnapshot();

            var result = assortment.BlockTemporaryFor(owner);

            Assert.That(result, Is.True);
            Assert.That(assortment.IsBlocked(), Is.True);
        }

        [Test]
        public void When_assortmentHasAnyBlock_then_dontLetAddingNewBlockAndKeepBlocking()
        {
            Assortment assortment = AnyPermanentBlockedAssortment();
            Owner owner = OwnerFromSnapshot();

            var result = assortment.BlockTemporaryFor(owner);

            Assert.That(result, Is.False);
            Assert.That(assortment.IsBlocked(), Is.True);
        }

        [Test]
        public void When_assortmentHasTemporaryBlock_then_UnblockAssortment()
        {
            Assortment assortment = AnyTemporaryBlockedAssortment();

            var result = assortment.Unblock();

            Assert.That(result, Is.True);
            Assert.That(assortment.IsBlocked(), Is.False);
        }

        [Test]
        public void When_assortmentHasPermanentBlock_then_CantUnblockAssortment()
        {
            Assortment assortment = AnyPermanentBlockedAssortment();

            var result = assortment.Unblock();

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_assortmentHasTemporaryBlockBySameOwner_then_BlockPermanently()
        {
            Assortment assortment = AnyTemporaryBlockedAssortment();
            Owner owner = OwnerFromSnapshot();

            var result = assortment.BlockPermanentlyFor(owner);

            Assert.That(result, Is.True);
            Assert.That(assortment.IsBlocked(), Is.True);
        }

        [Test]
        public void When_assortmentHasNotTemporaryBlock_then_CantBlockPermanently()
        {
            Assortment assortment = NotBlockedAssortment();
            Owner owner = OwnerFromSnapshot();

            var result = assortment.BlockPermanentlyFor(owner);

            Assert.That(result, Is.False);
            Assert.That(assortment.IsBlocked(), Is.False);
        }

        [Test]
        public void When_assortmentHasPermanentBlock_then_CantBlockPermanently()
        {
            Assortment assortment = AnyPermanentBlockedAssortment();
            Owner owner = OwnerFromSnapshot();

            var result = assortment.BlockPermanentlyFor(owner);

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_assortmentHasTemporaryBlockByOtherOwner_then_CantBlockPermanently()
        {
            Assortment assortment = AnyTemporaryBlockedAssortment();
            Owner owner = NewOwner();

            var result = assortment.BlockPermanentlyFor(owner);

            Assert.That(result, Is.False);
        }

        private Assortment NotBlockedAssortment() => Assortment.New();
        private Assortment AnyPermanentBlockedAssortment() => Assortment.FromSnapshot(Guid.NewGuid(), new Block[] { Block.Temporary(OwnerFromSnapshot()), Block.Permanent(OwnerFromSnapshot()) });
        private Assortment AnyTemporaryBlockedAssortment() => Assortment.FromSnapshot(Guid.NewGuid(), new Block[] { Block.Temporary(OwnerFromSnapshot()) });
        private Owner OwnerFromSnapshot() => Owner.FromSnapshot(_anyOwnerGuid);
        private Owner NewOwner() => Owner.New();


    }
}