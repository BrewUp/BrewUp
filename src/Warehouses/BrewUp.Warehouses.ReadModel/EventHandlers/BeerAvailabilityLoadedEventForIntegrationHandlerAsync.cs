using BrewUp.Shared.Messages.Sagas;
using BrewUp.Warehouses.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public sealed class BeerAvailabilityLoadedEventForIntegrationHandlerAsync : DomainEventHandlerAsync<BeerAvailabilityLoaded>
{
    private readonly IEventBus _eventBus;
    public BeerAvailabilityLoadedEventForIntegrationHandlerAsync(ILoggerFactory loggerFactory,
        IEventBus eventBus) : base(loggerFactory)
    {
        _eventBus = eventBus;
    }

    public override async Task HandleAsync(BeerAvailabilityLoaded @event, CancellationToken cancellationToken = new ())
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
        
        BeerAvailabilityCommunicated integrationEvent = new(@event.BeerId, correlationId, @event.BeerName, @event.Availability);
        await _eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}