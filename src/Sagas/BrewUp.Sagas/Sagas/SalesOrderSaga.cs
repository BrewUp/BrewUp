using BrewUp.Sagas.Messages.Commands;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using BrewUp.Shared.Messages.Sagas;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace BrewUp.Sagas.Sagas;

public sealed class SalesOrderSaga : Saga<SalesOrderSaga.SalesOrderSagaState>,
    ISagaStartedByAsync<StartSalesOrderSaga>,
    ISagaEventHandlerAsync<BeerOriginDiscovered>
{
    public class SalesOrderSagaState
    {
        public string SagaId { get; set; } = string.Empty;
        public OrderId OrderId { get; set; } = new(Guid.Empty);
        public int AvailabilityChecked { get; set; } = 0;
        public int OriginChecked { get; set; } = 0;
        public IEnumerable<BeerSalesOrderSagaRow> Rows { get; set; } = Enumerable.Empty<BeerSalesOrderSagaRow>();
    }

    public SalesOrderSaga(IServiceBus serviceBus, ISagaRepository repository, ILoggerFactory loggerFactory)
    : base(serviceBus, repository, loggerFactory)
    {
    }

    public async Task StartedByAsync(StartSalesOrderSaga command)
    {
        SagaState = new SalesOrderSagaState
        {
            SagaId = command.MessageId.ToString(),
            OrderId = command.OrderId,
            AvailabilityChecked = 0,
            OriginChecked = 0,
            Rows = command.Rows.Select(r => new BeerSalesOrderSagaRow(new BeerId(r.BeerId), new BeerName(r.BeerName), r.Quantity, new Availability(0, r.Quantity.UnitOfMeasure), new HomeBrewed(false)))
        };
        await Repository.SaveAsync(command.MessageId, SagaState);

        foreach (var sagaRow in SagaState.Rows)
        {
            AskForBeerAvailability askBeersAvailability = new(sagaRow.BeerId, command.MessageId, sagaRow.BeerName);
            await ServiceBus.SendAsync(askBeersAvailability, CancellationToken.None);
        }
    }

    public async Task HandleAsync(BeerAvailabilityCommunicated @event)
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
        if (correlationId.Equals(Guid.Empty))
            return;

        var sagaState = await Repository.GetByIdAsync<SalesOrderSagaState>(correlationId);
        var currentRow = sagaState.Rows.FirstOrDefault(b => b.BeerId.Equals(@event.BeerId));
        if (currentRow is null)
            return;
        var sagaRows = sagaState.Rows.Where(b => !b.BeerId.Equals(@event.BeerId));
        sagaRows = sagaRows.Concat(new List<BeerSalesOrderSagaRow>
        {
            currentRow with {Availability = @event.Availability}
        });
        sagaState.Rows = sagaRows;
        sagaState.AvailabilityChecked++;
        await Repository.SaveAsync(correlationId, sagaState);

        // Check if all rows have been checked
        var beerSalesOrderSagaRows = sagaState.Rows as BeerSalesOrderSagaRow[] ?? sagaState.Rows.ToArray();
        if (!sagaState.AvailabilityChecked.Equals(beerSalesOrderSagaRows.Length))
            return;

        foreach (var sagaRow in beerSalesOrderSagaRows)
        {
            AskForBeerOrigin askForBeerOrigin = new(sagaRow.BeerId, correlationId);
            await ServiceBus.SendAsync(askForBeerOrigin, CancellationToken.None);
        }
    }

    public async Task HandleAsync(BeerOriginDiscovered @event)
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
        if (correlationId.Equals(Guid.Empty))
            return;
        
        var sagaState = await Repository.GetByIdAsync<SalesOrderSagaState>(correlationId);
        var sagaRow = sagaState.Rows.FirstOrDefault(b => b.BeerId.Equals(@event.BeerId));
        if (sagaRow is null)
            return;

        sagaRow = sagaRow with {HomeBrewed = @event.HomeBrewed};
        var sagaRows = sagaState.Rows.Where(b => !b.BeerId.Equals(@event.BeerId));
        sagaRows = sagaRows.Concat(new List<BeerSalesOrderSagaRow>
        {
            sagaRow
        });
        sagaState.OriginChecked++;
        sagaState.Rows = sagaRows;
        await Repository.SaveAsync(correlationId, sagaState);
        
        var rowsArray = sagaState.Rows as BeerSalesOrderSagaRow[] ?? sagaState.Rows.ToArray();
        if (!sagaState.OriginChecked.Equals(rowsArray.Length))
            return;
        
        var productionOrderRows = rowsArray
            .Where(b => b.HomeBrewed.Value)
            .Select(b => new ProductionOrderRow(b.BeerId, b.BeerName, b.Quantity));
        var productionRowsArray = productionOrderRows as ProductionOrderRow[] ?? productionOrderRows.ToArray();
        if (productionRowsArray.Any())
        {
            CreateProductionOrder createProductionOrder = new(new ProductionOrderId(sagaState.OrderId.Value),
                correlationId,
                new ProductionOrderNumber($"{DateTime.UtcNow.Year:0000}{DateTime.UtcNow.Month:00}{DateTime.UtcNow.Day:00}-{DateTime.UtcNow.Hour:00}{DateTime.UtcNow.Minute:00}"), 
                new OrderDate(DateTime.UtcNow), productionRowsArray);
            await ServiceBus.SendAsync(createProductionOrder, CancellationToken.None);            
        }

        var purchaseOrderRows = rowsArray
            .Where(b => !b.HomeBrewed.Value)
            .Select(b => new PurchaseOrderRow(b.BeerId, b.BeerName, b.Quantity, new Cost(10, "Lt")));
        var purchaseRowsArray = purchaseOrderRows as PurchaseOrderRow[] ?? purchaseOrderRows.ToArray();
        if (purchaseRowsArray.Any())
        {
            CreatePurchaseOrder createPurchaseOrder = new (new PurchaseOrderId(sagaState.OrderId.Value),
                correlationId,
                new PurchaseOrderNumber(
                    $"{DateTime.UtcNow.Year:0000}{DateTime.UtcNow.Month:00}{DateTime.UtcNow.Day:00}-{DateTime.UtcNow.Hour:00}{DateTime.UtcNow.Minute:00}"),
                new SupplierId(Guid.NewGuid()),
                new OrderDate(DateTime.UtcNow), purchaseRowsArray);
            await ServiceBus.SendAsync(createPurchaseOrder, CancellationToken.None);
        }
    }
}