using BrewUp.Shared.Contracts;

namespace BrewUp.Warehouses.Facade;

public interface IWarehousesFacade
{
    Task LoadAvailabilityAsync(BeerAvailabilityJson body, CancellationToken cancellationToken);
}