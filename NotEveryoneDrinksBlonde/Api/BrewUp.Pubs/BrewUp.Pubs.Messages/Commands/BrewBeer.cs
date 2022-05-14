using BrewUp.Pubs.Module.Extensions.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Pubs.Messages.Commands;

public sealed class BrewBeer : Command
{
    public readonly BeerId BeerId;
    public readonly BeerType BeerType;
    public readonly BeerQuantity BeerQuantity;

    public BrewBeer(BeerId aggregateId, BeerType beerType, BeerQuantity beerQuantity)
        : base(aggregateId)
    {
        BeerId = aggregateId;
        BeerType = beerType;
        BeerQuantity = beerQuantity;
    }
}