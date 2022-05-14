using Microsoft.Extensions.DependencyInjection;
using Muflone.Factories;
using Muflone.Messages.Events;

namespace BrewUp.Production.Module.Factories
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactoryAsync
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDomainEventHandlerAsync<T> CreateDomainEventHandlerAsync<T>() where T : class, IDomainEvent
        {
            return _serviceProvider.GetService<IDomainEventHandlerAsync<T>>();
        }

        public IEnumerable<IDomainEventHandlerAsync<T>> CreateDomainEventHandlersAsync<T>() where T : class, IDomainEvent
        {
            return _serviceProvider.GetServices<IDomainEventHandlerAsync<T>>();
        }
    }
}