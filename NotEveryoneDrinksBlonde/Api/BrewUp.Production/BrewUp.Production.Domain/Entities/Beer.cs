using BrewUp.Production.Messages.Events;
using BrewUp.Production.Module.Extensions.CustomTypes;
using Muflone.Core;

namespace BrewUp.Production.Domain.Entities;

public class Beer : AggregateRoot
{
    private BeerType _beerType;
    private BeerQuantity _beerQuantity;

    protected Beer()
    {}

    internal static Beer BrewBeer(BeerId beerId, BeerType beerType, BeerQuantity beerQuantity)
    {
        return new Beer(beerId, beerType, beerQuantity);
    }

    private Beer(BeerId beerId, BeerType beerType, BeerQuantity beerQuantity)
    {
        RaiseEvent(new BeerBrewed(beerId, beerType, beerQuantity));
    }

    private void Apply(BeerBrewed @event)
    {
        Id = @event.BeerId;
        _beerType = @event.BeerType;
        _beerQuantity = @event.BeerQuantity;
    }
}