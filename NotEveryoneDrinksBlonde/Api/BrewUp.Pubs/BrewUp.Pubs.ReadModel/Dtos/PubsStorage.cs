using BrewUp.Pubs.Module.Extensions.CustomTypes;
using BrewUp.Pubs.Module.Extensions.JsonModel;
using BrewUp.Pubs.ReadModel.Abstracts;

namespace BrewUp.Pubs.ReadModel.Dtos;

public class PubsStorage : DtoBase
{
    public string PubName { get; private set; } = string.Empty;

    public IEnumerable<StorageJson> Storage { get; private set; } = Enumerable.Empty<StorageJson>();

    protected PubsStorage()
    {}

    public static PubsStorage CreatePubsStorage(PubId pubId, PubName pubName, BeerType beerType,
        BeerQuantity beerQuantity) => new(pubId.Value, pubName.Value);

    private PubsStorage(Guid pubId, string pubName)
    {
        Id = pubId.ToString();
        PubName = pubName;
    }

    public void IncreaseQuantity(BeerType beerType, BeerQuantity quantity)
    {
        var beerStorage = Storage.FirstOrDefault(s => s.BeerType.Equals(beerType.Value));
        var beerQuantity = quantity.Value;
        if (beerStorage != null)
        {
            beerQuantity += beerStorage.Quantity;
            Storage = Storage.Where(s => !s.BeerType.Equals(beerType.Value));
        }

        Storage = Storage.Concat(new List<StorageJson>
        {
            new()
            {
                BeerType = beerType.Value,
                Quantity = beerQuantity
            }
        });
    }

    public void DecreaseQuantity(BeerType beerType, BeerQuantity quantity)
    {
        var beerStorage = Storage.FirstOrDefault(s => s.BeerType.Equals(beerType.Value));
        if (beerStorage is null)
            return;

        var beerQuantity = beerStorage.Quantity - quantity.Value;
        Storage = Storage.Where(s => !s.BeerType.Equals(beerType.Value));

        Storage = Storage.Concat(new List<StorageJson>
        {
            new()
            {
                BeerType = beerType.Value,
                Quantity = beerQuantity
            }
        });
    }

    public PubsStorageJson ToJson() => new()
    {
        PubId = Id,
        PubName = PubName,

        Storage = Storage
    };

    public PubJson ToPubJson() => new()
    {
        PubId = Id,
        PubName = PubName
    };
}