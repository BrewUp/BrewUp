using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BrewUp.Production.Shared.Services;

public sealed class StartSubscriptions : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public StartSubscriptions(IServiceCollection services)
    {
        _serviceProvider ??= services.BuildServiceProvider();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        var registerHandlers = new RegisterHandlers(_serviceProvider);

        registerHandlers.RegisterCommandHandlers();
        registerHandlers.RegisterDomainEventHandlers();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }
}