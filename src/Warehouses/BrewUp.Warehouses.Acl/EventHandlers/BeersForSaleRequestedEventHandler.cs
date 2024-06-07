using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Messages.Sagas;
using BrewUp.Warehouses.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Acl.EventHandlers;

public sealed class BeersForSaleRequestedEventHandler : IntegrationEventHandlerAsync<BeersForSaleRequested>
{
    private readonly IServiceBus _serviceBus;
    
    public BeersForSaleRequestedEventHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus) : base(loggerFactory)
    {
        _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
    }

    public override async Task HandleAsync(BeersForSaleRequested @event, CancellationToken cancellationToken = new ())
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

        foreach (var row in @event.Rows)
        {
            WithdrawalBySalesOrder command = new(new BeerId(row.BeerId), correlationId, row.Quantity);
            await _serviceBus.SendAsync(command, cancellationToken);
        }
    }
}