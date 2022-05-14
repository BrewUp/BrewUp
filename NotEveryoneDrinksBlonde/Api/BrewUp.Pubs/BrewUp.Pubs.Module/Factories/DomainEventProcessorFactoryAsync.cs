using Microsoft.Extensions.DependencyInjection;
using Muflone.Azure.Factories;
using Muflone.Messages.Events;

namespace BrewUp.Pubs.Module.Factories
{
    public class DomainEventProcessorFactoryAsync : IDomainEventProcessorFactoryAsync
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventProcessorFactoryAsync(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDomainEventProcessorAsync<T> CreateDomainEventEventProcessorAsync<T>() where T : class, IDomainEvent
        {
            return _serviceProvider.GetService<IDomainEventProcessorAsync<T>>();
        }
    }
}