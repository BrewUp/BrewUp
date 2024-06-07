using BrewUp.Warehouses.Domain.Entities;
using BrewUp.Warehouses.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public sealed class LoadBeerAvailabilityCommandHandlerAsync : CommandHandlerBaseAsync<LoadBeerAvailability>
{
    public LoadBeerAvailabilityCommandHandlerAsync(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task ProcessCommand(LoadBeerAvailability command, CancellationToken cancellationToken = default)
    {
        var aggregate = await Repository.GetByIdAsync<BeerAvailability>(command.AggregateId.Value);
        aggregate.LoadAvailability(command.Availability, command.MessageId);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}