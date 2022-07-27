namespace PhoneRegistryDDD.IntegrationTests.Setup;

[CollectionDefinition(nameof(PhoneRegistryCollection))]
public class PhoneRegistryCollection : ICollectionFixture<PhoneRegistryApplicationFactory>
{
}