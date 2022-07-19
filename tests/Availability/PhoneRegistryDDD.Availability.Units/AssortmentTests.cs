using System;
using NUnit.Framework;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.ValueObjects;

namespace PhoneRegistryDDD.Availability.Units;

public class AssortmentTests
{
    [Test]
    public void When_assortmentHasNotBlocks_then_blockAssortment()
    {
        var assortment = NotBlockedAssortment();
        var owner = AnyOwner();

        var result = assortment.BlockTemporaryFor(owner);

        Assert.That(result, Is.True);
        Assert.That(assortment.IsBlocked(), Is.True);
    }

    [Test]
    public void When_assortmentHasAnyBlock_then_dontLetAddingNewBlockAndKeepBlocking()
    {
        var assortment = AnyPermanentBlockedAssortment();
        var owner = AnyOwner();

        var result = assortment.BlockTemporaryFor(owner);

        Assert.That(result, Is.False);
        Assert.That(assortment.IsBlocked(), Is.True);
    }

    [Test]
    public void When_assortmentHasTemporaryBlock_then_UnblockAssortment()
    {
        var assortment = AnyTemporaryBlockedAssortment();

        var result = assortment.Unblock();

        Assert.That(result, Is.True);
        Assert.That(assortment.IsBlocked(), Is.False);
    }

    [Test]
    public void When_assortmentHasPermanentBlock_then_CantUnblockAssortment()
    {
        var assortment = AnyPermanentBlockedAssortment();

        var result = assortment.Unblock();

        Assert.That(result, Is.False);
    }

    [Test]
    public void When_assortmentHasNotAnyBlock_then_BlockPermanently()
    {
        var assortment = NotBlockedAssortment();
        var owner = AnyOwner();

        var result = assortment.BlockPermanentlyFor(owner);

        Assert.That(result, Is.True);
        Assert.That(assortment.IsBlocked(), Is.True);
    }

    [Test]
    public void When_assortmentHasPermanentBlock_then_CantBlockPermanently()
    {
        var assortment = AnyPermanentBlockedAssortment();
        var owner = AnyOwner();

        var result = assortment.BlockPermanentlyFor(owner);

        Assert.That(result, Is.False);
    }

    [Test]
    public void When_assortmentHasTemporaryBlockByOtherOwner_then_CantBlockPermanent()
    {
        var assortment = AnyTemporaryBlockedAssortment();
        var owner = Owner.New(Guid.NewGuid());

        var result = assortment.BlockPermanentlyFor(owner);

        Assert.That(result, Is.False);
    }

    private static Assortment NotBlockedAssortment() => Assortment.New();

    private static Assortment AnyPermanentBlockedAssortment() =>
        Assortment.FromSnapshot(Guid.NewGuid(), new Block[] { Block.Temporary(AnyOwner()), Block.Permanent(AnyOwner()) });

    private static Assortment AnyTemporaryBlockedAssortment() => Assortment.FromSnapshot(Guid.NewGuid(), new Block[] { Block.Temporary(AnyOwner()) });
    private static Owner AnyOwner() => Owner.FromSnapshot(Guid.NewGuid());
}