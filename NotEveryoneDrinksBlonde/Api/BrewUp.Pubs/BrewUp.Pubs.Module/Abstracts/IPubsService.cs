using BrewUp.Pubs.Module.Extensions.CustomTypes;
using BrewUp.Pubs.Module.Extensions.JsonModel;

namespace BrewUp.Pubs.Module.Abstracts;

public interface IPubsService
{
    Task RequestBeerAsync(BeersJson beerToBrew);
    Task BrewBeerAsync(BeerId beerId, BeerType beerType, BeerQuantity beerQuantity);

    Task<IEnumerable<BeersJson>> GetBeersAsync();
}