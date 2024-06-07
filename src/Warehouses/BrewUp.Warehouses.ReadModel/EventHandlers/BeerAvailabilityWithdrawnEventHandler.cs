using BrewUp.Warehouses.Messages.Events;
using BrewUp.Warehouses.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public sealed class BeerAvailabilityWithdrawnEventHandler : DomainEventHandlerAsync<BeerAvailabilityWithdrawn>
{
    private readonly IBeerAvailabilityService _beerAvailabilityService;
    
    public BeerAvailabilityWithdrawnEventHandler(ILoggerFactory loggerFactory, IBeerAvailabilityService beerAvailabilityService) : base(loggerFactory)
    {
        _beerAvailabilityService = beerAvailabilityService ?? throw new ArgumentNullException(nameof(beerAvailabilityService));
    }

    public override async Task HandleAsync(BeerAvailabilityWithdrawn @event, CancellationToken cancellationToken = new ())
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _beerAvailabilityService.WithdrawalAvailabilityAsync(@event.BeerId, @event.Availability,
            cancellationToken);
    }
}