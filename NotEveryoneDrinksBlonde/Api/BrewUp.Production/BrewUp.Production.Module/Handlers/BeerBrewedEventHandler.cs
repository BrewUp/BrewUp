using BrewUp.Production.Messages.Events;
using BrewUp.Production.Module.Abstracts;
using BrewUp.Production.Shared.Services;
using Microsoft.Extensions.Logging;

namespace BrewUp.Production.Module.Handlers;

public sealed class BeerBrewedEventHandler : DomainEventHandlerAsync<BeerBrewed>
{
    private readonly IProductionService _productionService;

    public BeerBrewedEventHandler(ILoggerFactory loggerFactory, IProductionService productionService) : base(loggerFactory)
    {
        _productionService = productionService;
    }

    public override async Task HandleAsync(BeerBrewed @event, CancellationToken cancellationToken = new())
    {
        try
        {
            await _productionService.BrewBeerAsync(@event.BeerId, @event.BeerType, @event.BeerQuantity);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}