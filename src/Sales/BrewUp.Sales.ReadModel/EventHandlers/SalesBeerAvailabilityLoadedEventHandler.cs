using BrewUp.Sales.Messages.Events;
using BrewUp.Sales.ReadModel.Services;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.EventHandlers;

public sealed class SalesBeerAvailabilityLoadedEventHandler : DomainEventHandlerBase<SalesBeerAvailabilityLoaded>
{
    private readonly ISalesBeerAvailabilityService _salesBeerAvailabilityService;
    
    public SalesBeerAvailabilityLoadedEventHandler(ILoggerFactory loggerFactory,
        ISalesBeerAvailabilityService salesBeerAvailabilityService) : base(loggerFactory)
    {
        _salesBeerAvailabilityService = salesBeerAvailabilityService ?? throw new ArgumentNullException(nameof(salesBeerAvailabilityService));
    }

    public override async Task HandleAsync(SalesBeerAvailabilityLoaded @event, CancellationToken cancellationToken = new ())
    {
        await _salesBeerAvailabilityService.LoadSalesBeerAvailabilityAsync(@event.BeerId, @event.Availability,
            cancellationToken);
    }
}