using System;
using NUnit.Framework;
using PhoneRegistryDDD.Disposal.Core.Entities;

namespace PhoneRegistryDDD.Disposal.Unit
{
    internal class ProductToDisposeTests
    {
        [Test]
        public void When_DisposeProduct_then_DisposeThisProduct()
        {
            ProductToDispose product = ProductToDispose.Free(AnyGuid());

            bool result = product.Utilize();

            Assert.That(result, Is.True);
            Assert.That(product.Disposed(), Is.True);
        }

        [Test]
        public void When_TryDisposeDisposedProductAlready_then_CantDisposeThisProductAgain()
        {
            ProductToDispose product = ProductToDispose.Disposed(AnyGuid());

            bool result = product.Utilize();

            Assert.That(result, Is.False);
            Assert.That(product.Disposed(), Is.True);
        }

        [Test]
        public void When_TryDisposePurchasedProductAlready_then_CantDisposeThisProduct()
        {
            ProductToDispose product = ProductToDispose.Purchased(AnyGuid());

            bool result = product.Utilize();

            Assert.That(result, Is.False);
            Assert.That(product.Disposed(), Is.False);
        }

        [Test]
        public void When_TryDisposeUseddProductAlready_then_CantDisposeThisProduct()
        {
            ProductToDispose product = ProductToDispose.Used(AnyGuid());

            bool result = product.Utilize();

            Assert.That(result, Is.False);
            Assert.That(product.Disposed(), Is.False);
        }

        public Guid AnyGuid() => Guid.NewGuid();
    }
}
