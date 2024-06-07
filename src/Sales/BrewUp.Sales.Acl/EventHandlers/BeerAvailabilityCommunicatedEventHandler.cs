using BrewUp.Sales.Messages.Commands;
using BrewUp.Sales.ReadModel.Entities;
using BrewUp.Shared.Messages.Sagas;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Sales.Acl.EventHandlers;

public sealed class BeerAvailabilityCommunicatedEventHandler : IntegrationEventHandlerBase<BeerAvailabilityCommunicated>
{
    private readonly IServiceBus _serviceBus;
    private readonly IQueries<SalesBeerAvailability> _queries;
    
    public BeerAvailabilityCommunicatedEventHandler(ILoggerFactory loggerFactory,
        IServiceBus serviceBus, IQueries<SalesBeerAvailability> queries) : base(loggerFactory)
    {
        _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        _queries = queries ?? throw new ArgumentNullException(nameof(queries));
    }

    public override async Task HandleAsync(BeerAvailabilityCommunicated @event, CancellationToken cancellationToken = new())
    {
        var availability = await _queries.GetByIdAsync(@event.BeerId.Value.ToString(), cancellationToken);
        if (availability == null)
        {
            CreateSalesBeerAvailablity command = new(@event.BeerId, @event.BeerName, @event.Availability);
            await _serviceBus.SendAsync(command, cancellationToken);
        }
        else
        {
            LoadSalesBeerAvailability command = new (@event.BeerId, @event.Availability);
            await _serviceBus.SendAsync(command, cancellationToken);
        }
    }
}