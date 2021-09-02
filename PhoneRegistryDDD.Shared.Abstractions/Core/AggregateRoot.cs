namespace PhoneRegistryDDD.Shared.Abstractions.Core
{
    public abstract class AggregateRoot<T>
    {
        public T Id { get; protected set; }
        public int Version { get; protected set; }
        private bool _incrementedVersion;

        protected void IncrementVersion()
        {
            if (_incrementedVersion) return;

            Version++;
            _incrementedVersion = true;
        }
    }
}