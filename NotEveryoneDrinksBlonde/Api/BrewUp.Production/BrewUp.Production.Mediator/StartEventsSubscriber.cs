using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BrewUp.Production.Mediator;

public class StartEventsSubscriber : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public StartEventsSubscriber(IServiceCollection services)
    {
        _serviceProvider ??= services.BuildServiceProvider();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var registerHandlers = new RegisterHandlers(_serviceProvider);
        registerHandlers.RegisterDomainEventHandlers();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}