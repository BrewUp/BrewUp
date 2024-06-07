using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMq.Commands;

public sealed class LoadSalesBeerAvailabilityConsumer : CommandConsumerBase<LoadSalesBeerAvailability>
{
    protected override ICommandHandlerAsync<LoadSalesBeerAvailability> HandlerAsync { get; }
    
    public LoadSalesBeerAvailabilityConsumer(IRepository repository,
        IMufloneConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory) : base(repository, connectionFactory, loggerFactory)
    {
        HandlerAsync = new LoadSalesBeerAvailabilityCommandHandler(repository, loggerFactory);
    }
}