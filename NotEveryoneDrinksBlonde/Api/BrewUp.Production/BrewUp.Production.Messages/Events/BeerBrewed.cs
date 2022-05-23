using BrewUp.Production.Module.Extensions.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Production.Messages.Events;

public sealed class BeerBrewed : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BeerType BeerType;
    public readonly BeerQuantity BeerQuantity;

    public readonly PubId PubId;
    public readonly PubName PubName;

    public BeerBrewed(BeerId aggregateId, BeerType beerType, BeerQuantity beerQuantity,
        PubId pubId, PubName pubName)
        : base(aggregateId)
    {
        BeerId = aggregateId;
        BeerType = beerType;
        BeerQuantity = beerQuantity;

        PubId = pubId;
        PubName = pubName;
    }
}