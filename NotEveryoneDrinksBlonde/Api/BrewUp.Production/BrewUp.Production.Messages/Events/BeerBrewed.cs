using BrewUp.Production.Module.Extensions.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Production.Messages.Events;

public sealed class BeerBrewed : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BeerType BeerType;
    public readonly BeerQuantity BeerQuantity;

    public BeerBrewed(BeerId aggregateId, BeerType beerType, BeerQuantity beerQuantity)
        : base(aggregateId)
    {
        BeerId = aggregateId;
        BeerType = beerType;
        BeerQuantity = beerQuantity;
    }
}