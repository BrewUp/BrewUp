using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using BrewUp.Shared.Messages.Sagas;
using BrewUp.Warehouses.Messages.Events;
using Muflone.Core;

namespace BrewUp.Warehouses.Domain.Entities;

public sealed class BeerAvailability : AggregateRoot
{
    private BeerName _beerName;
    
    private Availability _availability = new Availability(0, string.Empty);
    
    protected BeerAvailability()
    {
    }

    #region CreateBeerAvailability
    internal static BeerAvailability CreateBeerAvailability(BeerId beerId, BeerName beerName)
    {
        return new BeerAvailability(beerId, beerName);
    }
    
    private BeerAvailability(BeerId beerId, BeerName beerName)
    {
        RaiseEvent(new BeerAvailabilityCreated(beerId, beerName));
    }
    
    private void Apply(BeerAvailabilityCreated @event)
    {
        Id = @event.AggregateId;
        _beerName = @event.BeerName;

        _availability = new Availability(0, string.Empty);
    }
    #endregion

    #region LoadAvailability
    internal void LoadAvailability(Availability availability, Guid correlationId)
    {
        RaiseEvent(new BeerAvailabilityLoaded(new BeerId(Id.Value), correlationId, availability));
    }
    
    private void Apply(BeerAvailabilityLoaded @event)
    {
        _availability = @event.Availability with {Value = _availability.Value + @event.Availability.Value};
    }
    #endregion

    #region ChkAvailability
    internal void ChkAvailability(Guid correlationId)
    {
        RaiseEvent(new BeerAvailabilityChecked(new BeerId(Id.Value), correlationId, _availability));
    }
    
    private void Apply(BeerAvailabilityChecked @event)
    {
        // do nothing;
    }
    #endregion
}