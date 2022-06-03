using BrewUp.Production.Domain.Abstracts;
using BrewUp.Production.Domain.Entities;
using BrewUp.Production.Messages.Commands;
using BrewUp.Production.Messages.Events;
using BrewUp.Production.Shared.Services;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;

namespace BrewUp.Production.Domain.CommandsHandler;

public sealed class BrewBeerCommandHandler : CommandHandlerAsync<BrewBeer>
{
    private readonly IEventBus _eventBus;

    public BrewBeerCommandHandler(IRepository repository, ILoggerFactory loggerFactory, IEventBus eventBus)
        : base(repository, loggerFactory)
    {
        _eventBus = eventBus;
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
            await _eventBus.PublishAsync(@event);

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