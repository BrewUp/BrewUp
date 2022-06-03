using Muflone;
using Muflone.Messages;
using Muflone.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Muflone.Azure.Factories;
using Muflone.Messages.Events;

namespace BrewUp.Production.Shared.Services;

public class ServiceBus : IHostedService, IServiceBus, IEventBus, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public ServiceBus(IServiceProvider serviceProvider,
        ILoggerFactory loggerFactory)
    {
        _serviceProvider = serviceProvider;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task SendAsync<T>(T command) where T : class, ICommand
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        var commandHandler = _serviceProvider.GetService<ICommandHandlerAsync<T>>();
        if (commandHandler is null)
        {
            _logger.LogError($"[ServiceBus.SendAsync] - No Command consumer for {command}");
            throw new Exception($"[ServiceBus.SendAsync] - No CommandHandler for {command}");
        }

        await commandHandler.HandleAsync(command);
    }

    public Task RegisterHandlerAsync<T>(Action<T> handler) where T : IMessage
    {
        return Task.CompletedTask;
    }

    public async Task PublishAsync(IMessage @event)
    {
        await PublishWithProcessorAsync((IDomainEvent)@event);
    }

    public async Task PublishWithProcessorAsync<T>(T @event) where T : IDomainEvent
    {
        var domainEventProcessor = _serviceProvider.GetService<IDomainEventProcessorAsync<T>>();
        if (domainEventProcessor == null)
        {
            _logger.LogError($"[Publish.PublishAsync] - No DomainEvent consumer for {@event}");
            throw new Exception($"[Publish.PublishAsync] - No DomainEvent consumer for {@event}");
        }
        await domainEventProcessor.PublishAsync(@event);
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

    #region Dispose
    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            //noop atm
        }
        _disposed = true;
    }
    #endregion
}