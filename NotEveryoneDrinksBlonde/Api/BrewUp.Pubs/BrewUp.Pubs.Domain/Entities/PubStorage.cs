using BrewUp.Pubs.Messages.Events;
using BrewUp.Pubs.Module.Extensions.CustomTypes;
using Muflone.Core;

namespace BrewUp.Pubs.Domain.Entities;

public class PubStorage : AggregateRoot
{
    private PubName _pubName;

    private IEnumerable<Storage> _storages = Enumerable.Empty<Storage>();

    protected PubStorage()
    {}

    internal static PubStorage
        CreatePubStorage(PubId pubId, PubName pubName, BeerType beerType, BeerQuantity quantity) =>
        new(pubId, pubName, beerType, quantity);

    private PubStorage(PubId pubId, PubName pubName, BeerType beerType, BeerQuantity quantity)
    {
        RaiseEvent(new StorageUploaded(pubId, pubName, beerType, quantity));
    }

    private void Apply(StorageUploaded @event)
    {
        Id = @event.PubId;
        _pubName = @event.PubName;

        _storages = Enumerable.Empty<Storage>();
    }
}