using BrewUp.Pubs.Domain.Abstracts;
using BrewUp.Pubs.Domain.Entities;
using BrewUp.Pubs.Messages.Commands;
using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Pubs.Domain.CommandsHandler;

public sealed class DrinkBeerCommandHandler : CommandHandlerAsync<DrinkBeer>
{
    public DrinkBeerCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(DrinkBeer command, CancellationToken cancellationToken = new ())
    {
        try
        {
            var pubStorage = await Repository.GetByIdAsync<PubStorage>(command.PubId.Value);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}