using BrewUp.Shared.Messages.Sagas;
using BrewUp.Warehouses.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public sealed class BeerAvailabilityCheckedEventHandler : DomainEventHandlerAsync<BeerAvailabilityChecked>
{
    private readonly IEventBus _eventBus;
    
    public BeerAvailabilityCheckedEventHandler(ILoggerFactory loggerFactory, IEventBus eventBus) : base(loggerFactory)
    {
        _eventBus = eventBus;
    }

    public override async Task HandleAsync(BeerAvailabilityChecked @event, CancellationToken cancellationToken = new ())
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
        
        await _eventBus.PublishAsync(new BeerAvailabilityCommunicated(@event.BeerId, correlationId, @event.BeerName, @event.Availability));
    }
}