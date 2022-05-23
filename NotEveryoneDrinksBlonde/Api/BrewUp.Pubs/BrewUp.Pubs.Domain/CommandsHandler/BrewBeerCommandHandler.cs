using BrewUp.Pubs.Domain.Abstracts;
using BrewUp.Pubs.Domain.Entities;
using BrewUp.Pubs.Messages.Commands;
using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Pubs.Domain.CommandsHandler;

public sealed class BrewBeerCommandHandler : CommandHandlerAsync<BrewBeer>
{
    public BrewBeerCommandHandler(IRepository repository, ILoggerFactory loggerFactory)
        : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(BrewBeer command, CancellationToken cancellationToken = new())
    {
        try
        {
            //TODO: Implement EventStore using Muflone.AggregateBase

            var beer = Beer.BrewBeer(command.BeerId, command.BeerType, command.BeerQuantity, command.PubId,
                command.PubName);

            await Repository.SaveAsync(beer, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}