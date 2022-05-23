using BrewUp.Pubs.Messages.Events;
using BrewUp.Pubs.Module.Abstracts;
using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.Logging;

namespace BrewUp.Pubs.Module.Handlers;

public sealed class BeerBrewedForPubEventHandler : DomainEventHandlerAsync<BeerBrewed>
{
    private readonly IPubsStorageService _pubsStorageService;

    public BeerBrewedForPubEventHandler(ILoggerFactory loggerFactory,
        IPubsStorageService pubsStorageService) : base(loggerFactory)
    {
        _pubsStorageService = pubsStorageService;
    }

    public override async Task HandleAsync(BeerBrewed @event, CancellationToken cancellationToken = new ())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            await _pubsStorageService.UploadPubStorageAsync(@event.PubId, @event.PubName, @event.BeerType,
                @event.BeerQuantity);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}