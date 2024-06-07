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
        var aggregate = await Repository.GetByIdAsync<BeerAvailability>(command.BeerId.Value);
        aggregate.ChkAvailability(command.MessageId);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}