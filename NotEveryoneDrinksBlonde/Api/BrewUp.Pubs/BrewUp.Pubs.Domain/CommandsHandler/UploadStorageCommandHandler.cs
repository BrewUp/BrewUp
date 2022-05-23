using BrewUp.Pubs.Domain.Abstracts;
using BrewUp.Pubs.Domain.Entities;
using BrewUp.Pubs.Messages.Commands;
using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Pubs.Domain.CommandsHandler;

public sealed class UploadStorageCommandHandler : CommandHandlerAsync<UploadStorage>
{
    public UploadStorageCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(UploadStorage command, CancellationToken cancellationToken = new ())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var pubStorage =
                PubStorage.CreatePubStorage(command.PubId, command.PubName, command.BeerType, command.BeerQuantity);

            await Repository.SaveAsync(pubStorage, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}