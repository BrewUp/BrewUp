using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUp.Sales.Acl.EventHandlers;

public abstract class IntegrationEventHandlerBase<T> : IntegrationEventHandlerAsync<T> where T : IntegrationEvent
{
    protected readonly ILogger Logger;

    protected IntegrationEventHandlerBase(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }
}