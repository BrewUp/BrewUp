using BrewUpWasm.Modules.Pubs.Extensions.CustomTypes;
using BrewUpWasm.Modules.Pubs.Extensions.JsonModel;

namespace BrewUpWasm.Modules.Pubs.Extensions.Abstracts;

public interface IPubsService
{
    Task OrderBeerAsync(OrderBeerJson order);
    Task<IEnumerable<BeerConsumedJson>> GetAvailableBeersAsync(PubId pubId);
    Task<IEnumerable<BeerConsumedJson>> GetBeerConsumedAsync();
}