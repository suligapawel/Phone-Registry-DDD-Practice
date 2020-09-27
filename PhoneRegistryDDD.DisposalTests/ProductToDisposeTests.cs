using NUnit.Framework;
using PhoneRegistryDDD.Disposal.Entities;
using System;

namespace PhoneRegistryDDD.DisposalTests
{
    internal class ProductToDisposeTests
    {
        [Test]
        public void When_DisposeProduct_then_DisposeThisProduct()
        {
            ProductToDispose Product = ProductToDispose.Free(AnyGuid());

            bool result = Product.Utilize();

            Assert.That(result, Is.True);
            Assert.That(Product.Disposed(), Is.True);
        }

        [Test]
        public void When_TryDisposeDisposedProductAlready_then_CantDisposeThisProductAgain()
        {
            ProductToDispose Product = ProductToDispose.Disposed(AnyGuid());

            bool result = Product.Utilize();

            Assert.That(result, Is.False);
            Assert.That(Product.Disposed(), Is.True);
        }

        [Test]
        public void When_TryDisposePurchasedProductAlready_then_CantDisposeThisProduct()
        {
            ProductToDispose Product = ProductToDispose.Purchased(AnyGuid());

            bool result = Product.Utilize();

            Assert.That(result, Is.False);
            Assert.That(Product.Disposed(), Is.False);
        }

        [Test]
        public void When_TryDisposeUseddProductAlready_then_CantDisposeThisProduct()
        {
            ProductToDispose Product = ProductToDispose.Used(AnyGuid());

            bool result = Product.Utilize();

            Assert.That(result, Is.False);
            Assert.That(Product.Disposed(), Is.False);
        }

        public Guid AnyGuid() => Guid.NewGuid();
    }
}
