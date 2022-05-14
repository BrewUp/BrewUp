using BrewUp.Production.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace BrewUp.Production.Module.Abstracts;

public abstract class BaseProductionService
{
    protected IPersister Persister;
    protected ILogger Logger;

    protected BaseProductionService(IPersister persister,
        ILoggerFactory loggerFactory)
    {
        Persister = persister;
        Logger = loggerFactory.CreateLogger(GetType());
    }
}