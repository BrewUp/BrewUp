using BrewUp.Pubs.Module.Extensions.CustomTypes;
using BrewUp.Pubs.Module.Extensions.JsonModel;

namespace BrewUp.Pubs.Module.Abstracts;

public interface IPubsStorageService
{
    Task UploadPubStorageAsync(PubId pubId, PubName pubName, BeerType beerType, BeerQuantity quantity);
    Task DownloadPubStorageAsync(PubId pubId, PubName pubName, BeerType beerType, BeerQuantity quantity);

    Task<IEnumerable<PubJson>> GetPubsAsync();
    Task<PubsStorageJson> GetPubStorageAsync(PubId pubId);
}