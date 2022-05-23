using BrewUp.Pubs.Module.Extensions.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Pubs.Messages.Commands;

public sealed class DrinkBeer : Command
{
    public readonly PubId PubId;

    public readonly BeerType BeerType;
    public readonly BeerQuantity BeerQuantity;

    public DrinkBeer(PubId aggregateId, BeerType beerType, BeerQuantity beerQuantity)
        : base(aggregateId)
    {
        PubId = aggregateId;
        
        BeerType = beerType;
        BeerQuantity = beerQuantity;
    }
}