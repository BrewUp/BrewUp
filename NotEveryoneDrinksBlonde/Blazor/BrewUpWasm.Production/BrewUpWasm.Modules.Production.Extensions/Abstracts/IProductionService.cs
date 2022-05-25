using BrewUpWasm.Modules.Production.Extensions.JsonModel;

namespace BrewUpWasm.Modules.Production.Extensions.Abstracts;

public interface IProductionService
{
    Task<IEnumerable<BeerJson>> GetBeersAsync();
}