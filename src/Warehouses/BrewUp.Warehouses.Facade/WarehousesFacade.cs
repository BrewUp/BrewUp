using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using BrewUp.Shared.ReadModel;
using BrewUp.Warehouses.Messages.Commands;
using BrewUp.Warehouses.ReadModel.Entities;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Facade;

public sealed class WarehousesFacade : IWarehousesFacade
{
    private readonly ILogger _logger;
    private readonly IQueries<BeerAvailability> _queries;
    private readonly IServiceBus _serviceBus;

    public WarehousesFacade(IQueries<BeerAvailability> queries, IServiceBus serviceBus, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType());
        
        _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
    }

    public async Task LoadAvailabilityAsync(BeerAvailabilityJson body, CancellationToken cancellationToken)
    {
        try
        {
            var availability = await _queries.GetByIdAsync(body.BeerId, cancellationToken);
            if (availability is null)
            {
                CreateBeerAvailablity command = new(new BeerId(new Guid(body.BeerId)), new BeerName(body.BeerName), body.Availability);
                await _serviceBus.SendAsync(command, cancellationToken);
            }
            else
            {
                LoadBeerAvailability command = new(new BeerId(new Guid(body.BeerId)), body.Availability);
                await _serviceBus.SendAsync(command, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("StackTrace: {stackTrace} - Source: {source}", ex.StackTrace, ex.Source);
            throw;
        }
    }
}