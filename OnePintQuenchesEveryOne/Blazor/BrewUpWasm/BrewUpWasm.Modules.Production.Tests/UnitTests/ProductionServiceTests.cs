using System.Threading.Tasks;
using BrewUpWasm.Modules.Production.Extensions.Abstracts;
using BrewUpWasm.Modules.Production.Extensions.Concretes;
using Xunit;

namespace BrewUpWasm.Modules.Production.Tests.UnitTests;

public class ProductionServiceTests
{
    [Fact]
    public async Task Can_Get_Greetings_From_Production()
    {
        var productionService = new ProductionService();

        var greetings = await productionService.SayHelloAsync();

        Assert.Equal("Hello from Production Module", greetings);
    }
}