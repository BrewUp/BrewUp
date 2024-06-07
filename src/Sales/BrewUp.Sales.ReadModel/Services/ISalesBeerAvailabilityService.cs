using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using BrewUp.Shared.Entities;

namespace BrewUp.Sales.ReadModel.Services;

public interface ISalesBeerAvailabilityService
{
    Task CreateSalesBeerAvailabilityAsync(BeerId beerId, BeerName beerName, Availability availability, CancellationToken cancellationToken = default);
    Task LoadSalesBeerAvailabilityAsync(BeerId beerId, Availability availability, CancellationToken cancellationToken = default);
    
    Task<PagedResult<BeerAvailabilityJson>> GetSalesBeerAvailabilitiesAsync(BeerId beerId, int page, int pageSize,
        CancellationToken cancellationToken = default);
}