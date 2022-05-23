using BrewUpWasm.Modules.Pubs.Extensions.CustomTypes;
using BrewUpWasm.Modules.Pubs.Extensions.JsonModel;

namespace BrewUpWasm.Modules.Pubs.Extensions.Abstracts;

public interface IPubsService
{
    Task OrderBeerAsync(BeerJson order);
    Task DrinkBeerAsync(BeerJson beerToDrink);

    Task<IEnumerable<BeerJson>> GetAvailableBeersAsync(PubId pubId);
    Task<IEnumerable<BeerConsumedJson>> GetBeerConsumedAsync();
}