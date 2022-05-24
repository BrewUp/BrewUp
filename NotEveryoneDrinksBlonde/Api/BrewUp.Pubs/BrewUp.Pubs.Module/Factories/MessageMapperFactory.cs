using Microsoft.Extensions.DependencyInjection;
using Muflone.Factories;
using Muflone.Messages;

namespace BrewUp.Pubs.Module.Factories
{
    public class MessageMapperFactory : IMessageMapperFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageMapperFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMessageMapper<T> CreateMessageMapper<T>() where T : class, IMessage
        {
            return _serviceProvider.GetService<IMessageMapper<T>>();
        }
    }
}