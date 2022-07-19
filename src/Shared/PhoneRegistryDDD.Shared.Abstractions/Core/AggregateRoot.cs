namespace PhoneRegistryDDD.Shared.Abstractions.Core;

public abstract class AggregateRoot<T>
{
    private bool _incrementedVersion;

    public T Id { get; protected set; }
    public int Version { get; protected set; }

    protected void IncrementVersion()
    {
        if (_incrementedVersion)
        {
            return;
        }

        Version++;
        _incrementedVersion = true;
    }
}