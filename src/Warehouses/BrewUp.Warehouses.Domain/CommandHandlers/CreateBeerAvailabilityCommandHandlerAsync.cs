using BrewUp.Warehouses.Domain.Entities;
using BrewUp.Warehouses.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public sealed class CreateBeerAvailabilityCommandHandlerAsync : CommandHandlerBaseAsync<CreateBeerAvailablity>
{
    public CreateBeerAvailabilityCommandHandlerAsync(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task ProcessCommand(CreateBeerAvailablity command, CancellationToken cancellationToken = default)
    {
        var aggregate = BeerAvailability.CreateBeerAvailability(command.BeerId, command.BeerName, command.Availability);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}