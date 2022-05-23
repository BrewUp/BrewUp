using BrewUp.Pubs.Module.Extensions.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Pubs.Messages.Events;

public sealed class BeerDrunk : DomainEvent
{
    public readonly PubId PubId;

    public readonly BeerType BeerType;
    public readonly BeerQuantity BeerQuantity;

    public BeerDrunk(PubId aggregateId, BeerType beerType, BeerQuantity beerQuantity) : base(aggregateId)
    {
        PubId = aggregateId;

        BeerType = beerType;
        BeerQuantity = beerQuantity;
    }
}