using BrewUp.Infrastructures.RabbitMq;
using BrewUp.Production.Infrastructures.RabbitMq.Events;
using BrewUp.Sagas.Infrastructures.RabbitMq.Commands;
using BrewUp.Sagas.Infrastructures.RabbitMq.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Sagas.Infrastructures.RabbitMq;

public static class RabbitMqHelper
{
    public static IServiceCollection AddRabbitMqForSagasModule(this IServiceCollection services,
        RabbitMqSettings rabbitMqSettings)
    {
        var serviceProvider = services.BuildServiceProvider();
        var repository = serviceProvider.GetRequiredService<IRepository>();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        var rabbitMqConfiguration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username,
            rabbitMqSettings.Password, rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.ExchangeEventName);
        var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMqConfiguration, loggerFactory);

        services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMqConfiguration);

        serviceProvider = services.BuildServiceProvider();
        var serviceBus = serviceProvider.GetRequiredService<IServiceBus>();
        var sagaRepository = serviceProvider.GetRequiredService<ISagaRepository>();
        var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
        consumers = consumers.Concat(new List<IConsumer>
        {
            new BeersForSaleRequestedConsumer(serviceBus, mufloneConnectionFactory, loggerFactory),
            new StartSalesOrderSagaConsumer(serviceBus, sagaRepository, repository, mufloneConnectionFactory, loggerFactory),
            new BeersOriginDiscoveredConsumer(serviceBus, sagaRepository, mufloneConnectionFactory, loggerFactory),
        });
        services.AddMufloneRabbitMQConsumers(consumers);

        return services;
    }
}