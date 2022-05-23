using Muflone;
using Muflone.Messages;
using Muflone.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BrewUp.Production.Shared.Services;

public class ServiceBus : IServiceBus, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public ServiceBus(IServiceProvider serviceProvider,
        ILoggerFactory loggerFactory)
    {
        _serviceProvider = serviceProvider;
        _logger = loggerFactory.CreateLogger(GetType());
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
}