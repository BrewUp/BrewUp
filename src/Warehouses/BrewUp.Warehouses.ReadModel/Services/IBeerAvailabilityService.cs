using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using BrewUp.Shared.Entities;

namespace BrewUp.Warehouses.ReadModel.Services;

public interface IBeerAvailabilityService
{
    Task CreateBeerAvailabilityAsync(BeerId beerId, BeerName beerName, Availability availability,
        CancellationToken cancellationToken = default);
    Task LoadBeerAvailabilityAsync(BeerId beerId, Availability availability,
        CancellationToken cancellationToken = default);
    Task WithdrawalAvailabilityAsync(BeerId beerId, Availability availability,
        CancellationToken cancellationToken = default);
    
    Task<PagedResult<BeerAvailabilityJson>> GetBeerAvailabilitiesAsync(BeerId beerId, int page, int pageSize,
        CancellationToken cancellationToken = default);
}