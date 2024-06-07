using BrewUp.Sales.Messages.Events;
using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMq.Events;

public sealed class SalesBeerAvailabilityLoadedConsumer : DomainEventsConsumerBase<SalesBeerAvailabilityLoaded>
{
    protected override IEnumerable<IDomainEventHandlerAsync<SalesBeerAvailabilityLoaded>> HandlersAsync { get; }
    
    public SalesBeerAvailabilityLoadedConsumer(ISalesBeerAvailabilityService salesBeerAvailabilityService,
        IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory) : base(connectionFactory, loggerFactory)
    {
        HandlersAsync = new List<IDomainEventHandlerAsync<SalesBeerAvailabilityLoaded>>
        {
            new SalesBeerAvailabilityLoadedEventHandler(loggerFactory, salesBeerAvailabilityService)
        };
    }
}