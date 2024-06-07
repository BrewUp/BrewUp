using BrewUp.Shared.Dtos;
using BrewUp.Shared.Messages.Sagas;
using BrewUp.Warehouses.Domain.Entities;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public sealed class AskForBeerAvailabilityCommandHandler : CommandHandlerBaseAsync<AskForBeerAvailability>
{
    public AskForBeerAvailabilityCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task ProcessCommand(AskForBeerAvailability command, CancellationToken cancellationToken = default)
    {
        // I Know ... I need checking for beerAvailability before create new one ... but I'm lazy, and this is a demo ...
        var aggregate = BeerAvailability.CreateBeerAvailability(command.BeerId, command.BeerName);
        aggregate.LoadAvailability(new Availability(0, "Lt"), command.MessageId);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}