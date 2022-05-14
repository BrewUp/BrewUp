using BrewUp.Pubs.Messages.Events;
using BrewUp.Pubs.Module.Abstracts;
using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.Logging;

namespace BrewUp.Pubs.Module.Handlers;

public sealed class BeerBrewedEventHandler : DomainEventHandlerAsync<BeerBrewed>
{
    private readonly IPubsService _pubsService;

    public BeerBrewedEventHandler(ILoggerFactory loggerFactory, IPubsService pubsService) : base(loggerFactory)
    {
        _pubsService = pubsService;
    }

    public override async Task HandleAsync(BeerBrewed @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _pubsService.BrewBeerAsync(@event.BeerId, @event.BeerType, @event.BeerQuantity);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}