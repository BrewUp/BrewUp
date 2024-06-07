using BrewUp.Sales.Messages.Events;
using BrewUp.Sales.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUp.Sales.ReadModel.EventHandlers;

public sealed class SalesBeerAvailabilityCreatedEventHandler : DomainEventHandlerAsync<SalesBeerAvailabilityCreated>
{
    private readonly ISalesBeerAvailabilityService _salesBeerAvailabilityService;
    
    public SalesBeerAvailabilityCreatedEventHandler(ILoggerFactory loggerFactory, 
        ISalesBeerAvailabilityService salesBeerAvailabilityService) : base(loggerFactory)
    {
        _salesBeerAvailabilityService = salesBeerAvailabilityService ?? throw new ArgumentNullException(nameof(salesBeerAvailabilityService));
    }

    public override async Task HandleAsync(SalesBeerAvailabilityCreated @event, CancellationToken cancellationToken = new())
    {
        await _salesBeerAvailabilityService.CreateSalesBeerAvailabilityAsync(@event.BeerId, @event.BeerName, @event.Availability, cancellationToken);
    }
}