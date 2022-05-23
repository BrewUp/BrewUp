using BrewUp.Pubs.Module.Extensions.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Pubs.Messages.Events;

public sealed class StorageUploaded : DomainEvent
{
    public readonly PubId PubId;
    public readonly PubName PubName;

    public readonly BeerType BeerType;
    public readonly BeerQuantity BeerQuantity;

    public StorageUploaded(PubId aggregateId, PubName pubName, BeerType beerType, BeerQuantity beerQuantity)
        : base(aggregateId)
    {
        PubId = aggregateId;
        PubName = pubName;

        BeerType = beerType;
        BeerQuantity = beerQuantity;
    }
}