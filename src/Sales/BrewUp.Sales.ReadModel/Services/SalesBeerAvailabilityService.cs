using BrewUp.Sales.ReadModel.Entities;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.Services;

public sealed class SalesBeerAvailabilityService : ServiceBase, ISalesBeerAvailabilityService
{
    private readonly IQueries<SalesBeerAvailability> _queries;
    
    public SalesBeerAvailabilityService(ILoggerFactory loggerFactory, IPersister persister, IQueries<SalesBeerAvailability> queries) : base(loggerFactory, persister)
    {
        _queries = queries;
        _queries.SetDatabaseName("BrewUp_Sales");
    }

    public async Task CreateSalesBeerAvailabilityAsync(BeerId beerId, BeerName beerName, Availability availability, CancellationToken cancellationToken = default)
    {
        try
        {
            var salesBeerAvailability = SalesBeerAvailability.CreateBeerAvailability(beerId, beerName, availability);
            await Persister.InsertAsync(salesBeerAvailability, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating beer availability");
            throw;
        }
    }

    public async Task LoadSalesBeerAvailabilityAsync(BeerId beerId, Availability availability, CancellationToken cancellationToken = default)
    {
        try
        {
            var beerAvailability = await Persister.GetByIdAsync<SalesBeerAvailability>(beerId.Value.ToString(), cancellationToken);
            beerAvailability.LoadBeerAvailability(availability);
            await Persister.UpdateAsync(beerAvailability, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating beer availability");
            throw;
        }
    }

    public async Task<PagedResult<BeerAvailabilityJson>> GetSalesBeerAvailabilitiesAsync(BeerId beerId, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var beerAvailability =
            await _queries.GetByFilterAsync(b => b.Id.Equals(beerId.Value.ToString()), 0, 100, cancellationToken);
        
        return beerAvailability.TotalRecords > 0
            ? new PagedResult<BeerAvailabilityJson>(beerAvailability.Results.Select(r => r.ToJson()), beerAvailability.Page, beerAvailability.PageSize, beerAvailability.TotalRecords)
            : new PagedResult<BeerAvailabilityJson>(Enumerable.Empty<BeerAvailabilityJson>(), 0, 0, 0);
    }
}