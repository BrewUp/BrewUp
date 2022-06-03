using Xunit;

namespace BrewUp.Production.Test;

[CollectionDefinition("Integration Fixture")]
public abstract class IntegrationCollectionFixture : ICollectionFixture<AppHttpClientFixture>
{
}