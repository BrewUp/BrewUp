using BrewUp.Production.Module.Extensions.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Production.Messages.Commands;

public sealed class BrewBeer : Command
{
    public readonly BeerId BeerId;
    public readonly BeerType BeerType;
    public readonly BeerQuantity BeerQuantity;

    public readonly PubId PubId;
    public readonly PubName PubName;

    public BrewBeer(BeerId aggregateId, BeerType beerType, BeerQuantity beerQuantity,
        PubId pubId, PubName pubName) : base(aggregateId)
    {
        BeerId = aggregateId;
        BeerType = beerType;
        BeerQuantity = beerQuantity;

        PubId = pubId;
        PubName = pubName;
    }
}