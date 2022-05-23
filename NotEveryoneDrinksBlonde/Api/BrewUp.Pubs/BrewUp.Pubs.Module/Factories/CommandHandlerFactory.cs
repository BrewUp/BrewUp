using Microsoft.Extensions.DependencyInjection;
using Muflone.Factories;
using Muflone.Messages.Commands;

namespace BrewUp.Pubs.Module.Factories
{
    public class CommandHandlerFactoryAsync : ICommandHandlerFactoryAsync
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerFactoryAsync(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandlerAsync<T> CreateCommandHandlerAsync<T>() where T : class, ICommand
        {
            return _serviceProvider.GetService<ICommandHandlerAsync<T>>();
        }
    }
}