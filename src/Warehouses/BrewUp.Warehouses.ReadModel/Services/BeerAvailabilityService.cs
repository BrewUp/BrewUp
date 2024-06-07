using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using BrewUp.Warehouses.ReadModel.Entities;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouses.ReadModel.Services;

public sealed class BeerAvailabilityService : ServiceBase, IBeerAvailabilityService
{
    private readonly IQueries<BeerAvailability> _queries;
    
    public BeerAvailabilityService(ILoggerFactory loggerFactory, IPersister persister, IQueries<BeerAvailability> queries) : base(loggerFactory, persister)
    {
        _queries = queries;
        _queries.SetDatabaseName("BrewUp_Warehouses");
    }

    public async Task CreateBeerAvailabilityAsync(BeerId beerId, BeerName beerName, Availability availability, CancellationToken cancellationToken = default)
    {
        try
        {
            var beerAvailability = BeerAvailability.CreateBeerAvailability(beerId, beerName, availability);
            await Persister.InsertAsync(beerAvailability, cancellationToken);    
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating beer availability");
            throw;
        }
    }

    public async Task LoadBeerAvailabilityAsync(BeerId beerId, Availability availability, CancellationToken cancellationToken = default)
    {
        try
        {
            var beerAvailability = await Persister.GetByIdAsync<BeerAvailability>(beerId.Value.ToString(), cancellationToken);
            beerAvailability.LoadBeerAvailability(availability);
            await Persister.UpdateAsync(beerAvailability, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error loading beer availability");
            throw;
        }
    }

    public async Task WithdrawalAvailabilityAsync(BeerId beerId, Availability availability,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var beerAvailability = await Persister.GetByIdAsync<BeerAvailability>(beerId.Value.ToString(), cancellationToken);
            beerAvailability.WithdrawalAvailability(availability);
            await Persister.UpdateAsync(beerAvailability, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error withdrawal beer availability");
            throw;
        }
    }

    public async Task<PagedResult<BeerAvailabilityJson>> GetBeerAvailabilitiesAsync(BeerId beerId, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var beerAvailability =
            await _queries.GetByFilterAsync(b => b.Id.Equals(beerId.Value.ToString()), 0, 100, cancellationToken);
        
        return beerAvailability.TotalRecords > 0
            ? new PagedResult<BeerAvailabilityJson>(beerAvailability.Results.Select(r => r.ToJson()), beerAvailability.Page, beerAvailability.PageSize, beerAvailability.TotalRecords)
            : new PagedResult<BeerAvailabilityJson>(Enumerable.Empty<BeerAvailabilityJson>(), 0, 0, 0);
    }
}