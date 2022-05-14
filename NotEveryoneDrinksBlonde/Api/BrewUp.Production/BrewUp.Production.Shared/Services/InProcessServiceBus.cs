using System.Linq.Expressions;
using Muflone;
using Muflone.Messages;
using Muflone.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Production.Shared.Services;

public sealed class InProcessServiceBus : IServiceBus, IDisposable
{
    private readonly Dictionary<Type, List<Action<IMessage>>> _routes = new ();
    private readonly IServiceProvider _serviceProvider;

    public InProcessServiceBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SendAsync<T>(T command) where T : class, ICommand
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        var commandHandler = _serviceProvider.GetService<ICommandHandlerAsync<T>>();
        if (commandHandler is null)
        {
            throw new Exception($"[InProcessServiceBus.SendAsync] - No CommandHandler for {command}");
        }

        await commandHandler.HandleAsync(command);
    }

    public Task RegisterHandlerAsync<T>(Action<T> handler) where T : IMessage
    {
        if (!_routes.TryGetValue(typeof(T), out var handlers))
        {
            handlers = new List<Action<IMessage>>();
            _routes.Add(typeof(T), handlers);
        }
        handlers.Add(DelegateAdjuster.CastArgument<IMessage, T>(x => handler(x)));

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

public static class DelegateAdjuster
{
    public static Action<TBase> CastArgument<TBase, TDerived>(Expression<Action<TDerived>> source)
        where TDerived : TBase
    {
        if (typeof(TDerived) == typeof(TBase))
            return (Action<TBase>)((Delegate)source.Compile());

        var sourceParameter = Expression.Parameter(typeof(TBase), "source");
        var result =
            Expression.Lambda<Action<TBase>>(
                Expression.Invoke(source, Expression.Convert(sourceParameter, typeof(TDerived))), sourceParameter);

        return result.Compile();
    }
}