using BrewUp.Production.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Azure.Factories;
using Muflone.Factories;

namespace BrewUp.Production.Mediator;

public static class CommandProcessorHelper
{
    public static IServiceCollection AddCommandProcessor(this IServiceCollection services,
        string servicebusConnectionString)
    {
        services.AddScoped(provider =>
        {
            var commandHandlerFactory = provider.GetService<ICommandHandlerFactoryAsync>();
            var messageMapper = provider.GetService<IMessageMapperFactory>();

            var brokerOptions = new BrokerOptions
            {
                ConnectionString = servicebusConnectionString,
                QueueName = nameof(BrewBeer)
            };

            var commandConsumerFactory =
                new ServiceBusCommandProcessorFactory<BrewBeer>(brokerOptions, messageMapper, commandHandlerFactory);
            return commandConsumerFactory.CommandProcessorAsync;
        });

        return services;
    }
}