using BrewUpWasm.Modules.Pubs.Extensions.JsonModel;

namespace BrewUpWasm.Modules.Pubs.Extensions.Abstracts;

public interface IPubsService
{
    Task OrderBeerAsync(OrderBeerJson order);
}