using NUnit.Framework;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.ValueObjects;
using System;

namespace PhoneRegistryDDD.Availability.CoreTests
{
    public class AssortmentTests
    {

        [Test]
        public void When_assortmentHasNotBlocks_then_blockAssortment()
        {
            Assortment assortment = NotBlockedAssortment();
            Owner owner = AnyOwner();

            var result = assortment.BlockTemporaryFor(owner);

            Assert.That(result, Is.True);
            Assert.That(assortment.IsBlocked(), Is.True);
        }


        [Test]
        public void When_assortmentHasAnyBlock_then_dontLetAddingNewBlockAndKeepBlocking()
        {
            Assortment assortment = AnyPermanentBlockedAssortment();
            Owner owner = AnyOwner();

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
        public void When_assortmentHasNotAnyBlock_then_BlockPermanently()
        {
            Assortment assortment = NotBlockedAssortment();
            Owner owner = AnyOwner();

            var result = assortment.BlockPermanentlyFor(owner);

            Assert.That(result, Is.True);
            Assert.That(assortment.IsBlocked(), Is.True);
        }

        [Test]
        public void When_assortmentHasPermanentBlock_then_CantBlockPermanently()
        {
            Assortment assortment = AnyPermanentBlockedAssortment();
            Owner owner = AnyOwner();

            var result = assortment.BlockPermanentlyFor(owner);

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_assortmentHasTemporaryBlockByOtherOwner_then_CantBlockPermanent()
        {
            Assortment assortment = AnyTemporaryBlockedAssortment();
            Owner owner = Owner.New();

            var result = assortment.BlockPermanentlyFor(owner);

            Assert.That(result, Is.False);
        }

        private Assortment NotBlockedAssortment() => Assortment.New();
        private Assortment AnyPermanentBlockedAssortment() => Assortment.FromSnapshot(Guid.NewGuid(), new Block[] { Block.Temporary(AnyOwner()), Block.Permanent(AnyOwner()) });
        private Assortment AnyTemporaryBlockedAssortment() => Assortment.FromSnapshot(Guid.NewGuid(), new Block[] { Block.Temporary(AnyOwner()) });
        private Owner AnyOwner() => Owner.FromSnapshot(Guid.NewGuid());
    }
}