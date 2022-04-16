using Microsoft.Extensions.Logging;

namespace BrewUpApi.Modules.Production.Abstracts;

public abstract class ProductionBaseService
{
    protected ILogger Logger;

    protected ProductionBaseService(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }
}