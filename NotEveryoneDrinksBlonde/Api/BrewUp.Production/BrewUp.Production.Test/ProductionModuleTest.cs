using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BrewUp.Production.Test;

[Collection("Integration Fixture")]
public class ProductionModuleTest
{
    private readonly AppHttpClientFixture _integrationFixture;

    public ProductionModuleTest(AppHttpClientFixture integrationFixture)
    {
        _integrationFixture = integrationFixture;
    }

    [Fact]
    public async Task Can_Get_Beer()
    {
        var getResult = await _integrationFixture.Client.GetAsync("/production/Beers");

        Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);
    }
}