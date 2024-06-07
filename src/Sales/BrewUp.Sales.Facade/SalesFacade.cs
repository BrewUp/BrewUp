using BrewUp.Sales.Messages.Commands;
using BrewUp.Sales.ReadModel.Entities;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.DomainIds;
using BrewUp.Sales.SharedKernel.Dtos;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using Muflone.Persistence;

namespace BrewUp.Sales.Facade;

public sealed class SalesFacade : ISalesFacade
{
    private readonly IServiceBus _serviceBus;
    private readonly ISalesOrderService _salesOrderService;
    private readonly IQueries<SalesBeerAvailability> _queries;

    public SalesFacade(IServiceBus serviceBus, ISalesOrderService salesOrderService, IQueries<SalesBeerAvailability> queries)
    {
        _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        _salesOrderService = salesOrderService ?? throw new ArgumentNullException(nameof(salesOrderService));
        _queries = queries ?? throw new ArgumentNullException(nameof(queries));
    }

    public async Task<string> CreateOrderAsync(SalesOrderJson body, CancellationToken cancellationToken)
    {
        if (body.SalesOrderId.Equals(Guid.Empty))
            body = body with {SalesOrderId = Guid.NewGuid()};

        // Check Availability in localstorage
        // N.B.: In a microservices solution SalesBeerAvailability should be out of date, because of EventualConsistency
        var beersAvailable = new List<SalesOrderRowJson>();
        foreach (var bodyRow in body.Rows)
        {
            var salesBeerAvailability = await _queries.GetByIdAsync(bodyRow.BeerId.ToString(), cancellationToken);
            if (salesBeerAvailability is not null && salesBeerAvailability.Availability.Value > 0)
                beersAvailable.Add(bodyRow);
        }
        
        CreateSalesOrder createSalesOrder = new(new SalesOrderId(body.SalesOrderId),
            new SalesOrderNumber(body.SalesOrderNumber),
            new PubId(body.PubId), new PubName(body.PubName), new OrderDate(body.OrderDate), 
            beersAvailable.Select(x => new SalesOrderRowDto(
                new BeerId(x.BeerId), new BeerName(x.BeerName), x.Quantity, x.Price)));

        await _serviceBus.SendAsync(createSalesOrder, cancellationToken);

        return body.SalesOrderId.ToString();
    }

    public async Task<PagedResult<SalesOrderJson>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        return await _salesOrderService.GetSalesOrdersAsync(0, 100, cancellationToken);
    }
}