using BrewUp.Production.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Azure.Factories;
using Xunit;

namespace BrewUp.Production.Mediator.Tests
{
    public class MessagesProcessorTest : BaseTest
    {
        [Fact]
        public void Can_Use_CommandProcessor()
        {
            var brewBeerCommandProcessor = ServiceProvider.GetService<ICommandProcessorAsync<BrewBeer>>();

            Assert.NotNull(brewBeerCommandProcessor);
        }
    }
}