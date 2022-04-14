using BrewUpWasm.Modules.Production.Extensions.Abstracts;

namespace BrewUpWasm.Modules.Production.Extensions.Concretes;

public sealed class ProductionService : IProductionService
{
    public Task<string> SayHelloAsync()
    {
        var greetings = "Hello from Production Module";

        return Task.FromResult(greetings);
    }
}