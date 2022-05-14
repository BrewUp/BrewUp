using BrewUp.Pubs.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace BrewUp.Pubs.Module.Abstracts;

public abstract class BasePubsService
{
    protected IPersister Persister;
    protected ILogger Logger;

    protected BasePubsService(IPersister persister,
        ILoggerFactory loggerFactory)
    {
        Persister = persister;
        Logger = loggerFactory.CreateLogger(GetType());
    }
}