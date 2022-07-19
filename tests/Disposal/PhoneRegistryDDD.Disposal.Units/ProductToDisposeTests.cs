using System;
using NUnit.Framework;
using PhoneRegistryDDD.Disposal.Core.Entities;

namespace PhoneRegistryDDD.Disposal.Units;

[TestFixture]
public class ProductToDisposeTests
{
    [Test]
    public void When_DisposeProduct_then_DisposeThisProduct()
    {
        var product = ProductToDispose.Free(AnyGuid());

        var result = product.Utilize();

        Assert.That(result, Is.True);
        Assert.That(product.Disposed(), Is.True);
    }

    [Test]
    public void When_TryDisposeDisposedProductAlready_then_CantDisposeThisProductAgain()
    {
        var product = ProductToDispose.Disposed(AnyGuid());

        var result = product.Utilize();

        Assert.That(result, Is.False);
        Assert.That(product.Disposed(), Is.True);
    }

    [Test]
    public void When_TryDisposePurchasedProductAlready_then_CantDisposeThisProduct()
    {
        var product = ProductToDispose.Purchased(AnyGuid());

        var result = product.Utilize();

        Assert.That(result, Is.False);
        Assert.That(product.Disposed(), Is.False);
    }

    [Test]
    public void When_TryDisposeUseddProductAlready_then_CantDisposeThisProduct()
    {
        var product = ProductToDispose.Used(AnyGuid());

        var result = product.Utilize();

        Assert.That(result, Is.False);
        Assert.That(product.Disposed(), Is.False);
    }

    private static Guid AnyGuid() => Guid.NewGuid();
}