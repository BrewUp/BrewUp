using BrewUp.Pubs.Module.Extensions.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Pubs.Messages.Commands;

public sealed class UploadStorage : Command
{
    public readonly PubId PubId;
    public readonly PubName PubName;

    public readonly BeerType BeerType;
    public readonly BeerQuantity BeerQuantity;

    public UploadStorage(PubId aggregateId, PubName pubName, BeerType beerType, BeerQuantity beerQuantity)
        : base(aggregateId)
    {
        PubId = aggregateId;
        PubName = pubName;

        BeerType = beerType;
        BeerQuantity = beerQuantity;
    }
}