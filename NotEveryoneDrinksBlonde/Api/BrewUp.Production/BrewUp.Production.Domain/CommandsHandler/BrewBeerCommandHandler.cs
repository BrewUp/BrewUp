using BrewUp.Production.Domain.Abstracts;
using BrewUp.Production.Domain.Entities;
using BrewUp.Production.Messages.Commands;
using BrewUp.Production.Messages.Events;
using BrewUp.Production.Shared.Abstracts;
using BrewUp.Production.Shared.Services;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Production.Domain.CommandsHandler;

public sealed class BrewBeerCommandHandler : CommandHandlerAsync<BrewBeer>
{
    private readonly IPublish _publish;

    public BrewBeerCommandHandler(IRepository repository, ILoggerFactory loggerFactory, IPublish publish)
        : base(repository, loggerFactory)
    {
        _publish = publish;
    }

    public override async Task HandleAsync(BrewBeer command, CancellationToken cancellationToken = new())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            //TODO: Implement EventStore using Muflone.AggregateBase

            var @event = new BeerBrewed(command.BeerId, command.BeerType, command.BeerQuantity, command.PubId,
                command.PubName);
            await _publish.PublishAsync(@event);

            var beer = Beer.BrewBeer(command.BeerId, command.BeerType, command.BeerQuantity, command.PubId,
                command.PubName);

            //await Repository.SaveAsync(beer, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}