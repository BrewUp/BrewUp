using BrewUp.Warehouses.Messages.Events;
using BrewUp.Warehouses.ReadModel.EventHandlers;
using BrewUp.Warehouses.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Events;

public sealed class BeerAvailabilityCreatedConsumer : DomainEventsConsumerBase<BeerAvailabilityCreated>
{
    protected override IEnumerable<IDomainEventHandlerAsync<BeerAvailabilityCreated>> HandlersAsync { get; }

    public BeerAvailabilityCreatedConsumer(IBeerAvailabilityService beerAvailabilityService,
        IEventBus eventBus,
        IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory) 
        : base(connectionFactory, loggerFactory)
    {

        HandlersAsync = new List<IDomainEventHandlerAsync<BeerAvailabilityCreated>>
        {
            new BeerAvailabilityCreatedEventHandlerAsync(loggerFactory, beerAvailabilityService),
            new BeerAvailabilityCreatedForIntegrationEventHandlerAsync(loggerFactory, eventBus)
        };
    }
}