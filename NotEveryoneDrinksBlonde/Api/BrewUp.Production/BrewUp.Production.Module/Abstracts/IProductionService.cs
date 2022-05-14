using BrewUp.Production.Module.Extensions.CustomTypes;
using BrewUp.Production.Module.Extensions.JsonModel;

namespace BrewUp.Production.Module.Abstracts;

public interface IProductionService
{
    Task PrepareBeerAsync(BeersJson beerToBrew);
    Task BrewBeerAsync(BeerId beerId, BeerType beerType, BeerQuantity beerQuantity);

    Task<IEnumerable<BeersJson>> GetBeersAsync();
}