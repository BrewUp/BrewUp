using BrewUp.Production.Messages.Events;
using BrewUp.Production.Module.Extensions.CustomTypes;
using Muflone.Core;

namespace BrewUp.Production.Domain.Entities;

public class Beer : AggregateRoot
{
    private BeerType _beerType = default!;
    private BeerQuantity _beerQuantity = default!;

    private PubId _pubId = default!;
    private PubName _pubName = default!;

    protected Beer()
    {}

    internal static Beer BrewBeer(BeerId beerId, BeerType beerType, BeerQuantity beerQuantity, PubId pubId,
        PubName pubName) => new (beerId, beerType, beerQuantity, pubId, pubName);

    private Beer(BeerId beerId, BeerType beerType, BeerQuantity beerQuantity, PubId pubId, PubName pubName)
    {
        // TODO: Command validation
        RaiseEvent(new BeerBrewed(beerId, beerType, beerQuantity, pubId, pubName));
    }

    private void Apply(BeerBrewed @event)
    {
        Id = @event.BeerId;

        _beerType = @event.BeerType;
        _beerQuantity = @event.BeerQuantity;

        _pubId = @event.PubId;
        _pubName = @event.PubName;
    }
}