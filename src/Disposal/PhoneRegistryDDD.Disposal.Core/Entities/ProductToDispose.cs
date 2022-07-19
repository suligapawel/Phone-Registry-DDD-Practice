using System;

namespace PhoneRegistryDDD.Disposal.Core.Entities;

public sealed class ProductToDispose
{
    private readonly Guid _id;
    private readonly bool _hasOwner;
    private readonly bool _purchased;
    private bool _disposed;

    private ProductToDispose(Guid id, bool hasOwner, bool disposed, bool purchased)
    {
        _id = id;
        _hasOwner = hasOwner;
        _disposed = disposed;
        _purchased = purchased;
    }

    public static ProductToDispose Free(Guid id) => new(id, false, false, false);
    public static ProductToDispose Used(Guid id) => new(id, true, false, false);
    public static ProductToDispose Disposed(Guid id) => new(id, false, true, false);
    public static ProductToDispose Purchased(Guid id) => new(id, false, false, true);

    public bool Utilize()
    {
        if (Disposed() || Purchased() || StillUsed())
        {
            return false;
        }

        _disposed = true;
        return true;
    }

    public bool Disposed() => _disposed;
    private bool Purchased() => _purchased;
    private bool StillUsed() => _hasOwner;
}