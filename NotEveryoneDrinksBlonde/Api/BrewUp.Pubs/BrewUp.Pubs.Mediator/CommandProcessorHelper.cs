using BrewUp.Pubs.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Azure.Factories;

namespace BrewUp.Pubs.Mediator;

public static class CommandProcessorHelper
{
    public static IServiceCollection AddCommandProcessor(this IServiceCollection services, string servicebusConnectionString)
    {
        services.AddScoped(_ =>
        {
            var brokerOptions = new BrokerOptions
            {
                ConnectionString = servicebusConnectionString,
                QueueName = nameof(BrewBeer).ToLower()
            };

            var commandConsumerFactory = new ServiceBusCommandProcessorFactory<BrewBeer>(brokerOptions);
            return commandConsumerFactory.CommandProcessorAsync;
        });

        return services;
    }
}