using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUp.Pubs.Module.Handlers
{
    public abstract class DomainEventHandlerAsync<T> : IDomainEventHandlerAsync<T> where T : class, IDomainEvent
    {
        protected readonly ILogger Logger;

        protected DomainEventHandlerAsync(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
        }

        public abstract Task HandleAsync(T @event, CancellationToken cancellationToken = new());

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DomainEventHandlerAsync()
        {
            Dispose(false);
        }
        #endregion
    }
}