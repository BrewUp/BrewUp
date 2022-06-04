using Muflone;
using Muflone.Messages;
using Muflone.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Azure.Factories;
using Muflone.Messages.Events;

namespace BrewUp.Pubs.Shared.Services;

public class ServiceBus : IServiceBus, IEventBus, IDisposable
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
        var commandConsumer = this._serviceProvider.GetService<ICommandProcessorAsync<T>>();
        if (commandConsumer == null)
        {
            _logger.LogError($"[ServiceBus.SendAsync] - No Command consumer for {command}");
            throw new Exception($"[ServiceBus.SendAsync] - No Command consumer for {command}");
        }
        await commandConsumer.SendAsync(command);
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