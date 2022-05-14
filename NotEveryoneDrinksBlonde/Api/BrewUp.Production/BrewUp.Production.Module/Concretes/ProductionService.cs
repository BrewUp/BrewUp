using BrewUp.Production.Messages.Events;
using BrewUp.Production.Module.Abstracts;
using BrewUp.Production.Module.Extensions.CustomTypes;
using BrewUp.Production.Module.Extensions.JsonModel;
using BrewUp.Production.ReadModel.Abstracts;
using BrewUp.Production.ReadModel.Dtos;
using BrewUp.Production.Shared.Abstracts;
using BrewUp.Production.Shared.Services;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BrewUp.Production.Module.Concretes;

public sealed class ProductionService : BaseProductionService, IProductionService
{
    private readonly IServiceBus _serviceBus;
    private readonly IPublish _publish;

    public ProductionService(IPersister persister,
        ILoggerFactory loggerFactory,
        IServiceBus serviceBus, IPublish publish) : base(persister, loggerFactory)
    {
        _serviceBus = serviceBus;
        _publish = publish;
    }

    public async Task PrepareBeerAsync(BeersJson beerToBrew)
    {
        try
        {
            //var brewBeer = new BrewBeer(new BeerId(Guid.NewGuid()), new BeerType(beerToBrew.BeerType),
            //    new BeerQuantity(beerToBrew.Quantity));
            //await _serviceBus.SendAsync(brewBeer);

            var beerBrewed = new BeerBrewed(new BeerId(Guid.NewGuid()), new BeerType(beerToBrew.BeerType),
                new BeerQuantity(beerToBrew.Quantity));
            await _publish.PublishAsync(beerBrewed);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task BrewBeerAsync(BeerId beerId, BeerType beerType, BeerQuantity beerQuantity)
    {
        try
        {
            var beer = await Persister.GetByIdAsync<Beers>(beerId.ToString());
            if (beer != null && !string.IsNullOrWhiteSpace(beer.Id))
                return;

            beer = Beers.CreateBeers(beerId, beerType, beerQuantity);
            await Persister.InsertAsync(beer);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<IEnumerable<BeersJson>> GetBeersAsync()
    {
        try
        {
            var beers = await Persister.FindAsync<Beers>();
            var beersArray = beers as Beers[] ?? beers.ToArray();

            return beersArray.Any()
                ? beersArray.Select(b => b.ToJson())
                : Enumerable.Empty<BeersJson>();
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}