using BrewUp.Pubs.Module.Abstracts;
using BrewUp.Pubs.Module.Extensions.CustomTypes;
using BrewUp.Pubs.Module.Extensions.JsonModel;
using BrewUp.Pubs.ReadModel.Abstracts;
using BrewUp.Pubs.ReadModel.Dtos;
using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.Logging;

namespace BrewUp.Pubs.Module.Concretes;

public sealed class PubsStorageService : BasePubsService, IPubsStorageService
{
    public PubsStorageService(IPersister persister, ILoggerFactory loggerFactory)
        : base(persister, loggerFactory)
    {
    }

    public async Task UploadPubStorageAsync(PubId pubId, PubName pubName, BeerType beerType, BeerQuantity quantity)
    {
        try
        {
            var pubStorage = await Persister.GetByIdAsync<PubsStorage>(pubId.ToString());
            if (pubStorage is null)
            {
                pubStorage = PubsStorage.CreatePubsStorage(pubId, pubName, beerType, quantity);
                pubStorage.IncreaseQuantity(beerType, quantity);
                await Persister.InsertAsync(pubStorage);
            }
            else
            {
                pubStorage.IncreaseQuantity(beerType, quantity);
                var propertiesToUpdate = new Dictionary<string, object>
                {
                    { "Storage", pubStorage.Storage }
                };

                await Persister.UpdateOneAsync<PubsStorage>(pubStorage.Id, propertiesToUpdate);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task DownloadPubStorageAsync(PubId pubId, PubName pubName, BeerType beerType, BeerQuantity quantity)
    {
        try
        {
            var pubStorage = await Persister.GetByIdAsync<PubsStorage>(pubId.ToString());
            if (pubStorage is null)
                return;

            pubStorage.DecreaseQuantity(beerType, quantity);
            var propertiesToUpdate = new Dictionary<string, object>
            {
                { "Storage", pubStorage.Storage }
            };

            await Persister.UpdateOneAsync<PubsStorage>(pubStorage.Id, propertiesToUpdate);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<IEnumerable<PubJson>> GetPubsAsync()
    {
        try
        {
            var pubStorage = await Persister.FindAsync<PubsStorage>();
            var pubsArray = pubStorage as PubsStorage[] ?? pubStorage.ToArray();

            return pubsArray != null
                ? pubsArray.Select(p => p.ToPubJson())
                : Enumerable.Empty<PubJson>();
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<PubsStorageJson> GetPubStorageAsync(PubId pubId)
    {
        try
        {
            var pubStorage = await Persister.GetByIdAsync<PubsStorage>(pubId.ToString());

            return pubStorage != null
                ? pubStorage.ToJson()
                : new PubsStorageJson();
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}