using BrewUpApi.Modules.Production.Abstracts;
using Microsoft.Extensions.Logging;

namespace BrewUpApi.Modules.Production.Concretes;

public sealed class ProductionService : ProductionBaseService, IProductionService
{
    public ProductionService(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    public Task<string> SayHelloAsync()
    {
        return Task.FromResult("Hello from Production Module");
    }
}