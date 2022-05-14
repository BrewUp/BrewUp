using BrewUp.Pubs.Messages.Commands;
using BrewUp.Pubs.Module.Abstracts;
using BrewUp.Pubs.Module.Extensions.CustomTypes;
using BrewUp.Pubs.Module.Extensions.JsonModel;
using BrewUp.Pubs.ReadModel.Abstracts;
using BrewUp.Pubs.ReadModel.Dtos;
using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BrewUp.Pubs.Module.Concretes;

public sealed class PubsService : BasePubsService, IPubsService
{
    private readonly IServiceBus _serviceBus;

    public PubsService(IPersister persister,
        ILoggerFactory loggerFactory,
        IServiceBus serviceBus) : base(persister, loggerFactory)
    {
        _serviceBus = serviceBus;
    }

    public async Task PrepareBeerAsync(BeersJson beerToBrew)
    {
        try
        {
            var brewBeer = new BrewBeer(new BeerId(Guid.NewGuid()), new BeerType(beerToBrew.BeerType),
                new BeerQuantity(beerToBrew.Quantity));
            await _serviceBus.SendAsync(brewBeer);
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