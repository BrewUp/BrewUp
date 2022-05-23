using Microsoft.Extensions.DependencyInjection;
using Muflone.Azure.Factories;
using Muflone.Messages.Commands;

namespace BrewUp.Pubs.Module.Factories
{
    public class CommandProcessorFactoryAsync : ICommandProcessorFactoryAsync
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandProcessorFactoryAsync(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandProcessorAsync<T> CreateCommandProcessorAsync<T>() where T : class, ICommand
        {
            return _serviceProvider.GetService<ICommandProcessorAsync<T>>();
        }
    }
}