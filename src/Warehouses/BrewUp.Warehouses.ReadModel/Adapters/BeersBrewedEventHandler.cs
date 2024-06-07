using BrewUp.Shared.Dtos;
using BrewUp.Shared.Messages;
using BrewUp.Warehouses.Messages.Commands;
using BrewUp.Warehouses.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BrewUp.Warehouses.ReadModel.Adapters;

public sealed class BeersBrewedEventHandler : IntegrationEventHandlerAsync<BeersBrewed>
{
    private readonly IServiceBus _serviceBus;
    private readonly IEventBus _eventBus;
    private readonly IBeerAvailabilityService _beerAvailabilityService;

    public BeersBrewedEventHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus,
        IEventBus eventBus, IBeerAvailabilityService beerAvailabilityService) : base(loggerFactory)
    {
        _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _beerAvailabilityService = beerAvailabilityService?? throw new ArgumentNullException(nameof(beerAvailabilityService));
    }

    /// <summary>
    /// If we try to manage Create and Load in the same handler, we will have a problem with the order of the messages.
    /// For sure we need some kind of saga to manage the order of the messages.
    /// </summary>
    /// <param name="event"></param>
    /// <param name="cancellationToken"></param>
    public override async Task HandleAsync(BeersBrewed @event, CancellationToken cancellationToken = new ())
    {
        foreach (var row in @event.Rows)
        {
            var beerAvailability = await _beerAvailabilityService.GetBeerAvailabilitiesAsync(row.BeerId, 0, 100, cancellationToken);
            if (beerAvailability.TotalRecords == 0)
            {
                CreateBeerAvailablity createBeerAvailablity = new(row.BeerId, row.BeerName, new Availability(0, "Lt"));
                await _serviceBus.SendAsync(createBeerAvailablity, cancellationToken);
                
                Thread.Sleep(200);
                LoadBeerAvailability loadBeerAvailabilityAfterCreated = new(row.BeerId,
                    new Availability((double)row.Quantity.Value, row.Quantity.UnitOfMeasure));
                await _serviceBus.SendAsync(loadBeerAvailabilityAfterCreated, cancellationToken);
                
                return;
            }

            LoadBeerAvailability loadBeerAvailability = new(row.BeerId,
                new Availability((double)row.Quantity.Value, row.Quantity.UnitOfMeasure));
            await _serviceBus.SendAsync(loadBeerAvailability, cancellationToken);
        }
        
        BeersForSalesOrderAvailable beersAvailable = new(@event.OrderId, @event.Rows);
        await _eventBus.PublishAsync(beersAvailable, cancellationToken);
    }
}