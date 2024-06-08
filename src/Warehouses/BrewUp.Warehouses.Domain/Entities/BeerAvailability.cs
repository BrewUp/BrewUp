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
    internal static BeerAvailability CreateBeerAvailability(BeerId beerId, BeerName beerName, Availability availability)
    {
        return new BeerAvailability(beerId, beerName, availability);
    }
    
    private BeerAvailability(BeerId beerId, BeerName beerName, Availability availability)
    {
        RaiseEvent(new BeerAvailabilityCreated(beerId, beerName, availability));
    }
    
    private void Apply(BeerAvailabilityCreated @event)
    {
        Id = @event.AggregateId;
        _beerName = @event.BeerName;

        _availability = @event.Availability;
    }
    #endregion

    #region LoadAvailability
    internal void LoadAvailability(Availability availability, Guid correlationId)
    {
        availability = availability with {Value = _availability.Value + availability.Value};
        RaiseEvent(new BeerAvailabilityLoaded(new BeerId(Id.Value), correlationId, _beerName, availability));
    }
    
    private void Apply(BeerAvailabilityLoaded @event)
    {
        _availability = @event.Availability with {Value = _availability.Value + @event.Availability.Value};
    }
    #endregion

    #region Withdrawal

    internal void WithdrawalBeer(Quantity quantity, Guid correlationId)
    {
        var availability = _availability with {Value = _availability.Value - quantity.Value};
        RaiseEvent(new BeerAvailabilityWithdrawn(new BeerId(Id.Value), correlationId, _beerName, availability));
    }

    private void Apply(BeerAvailabilityWithdrawn @event)
    {
        _availability = @event.Availability;
    }
    #endregion

    #region ChkAvailability
    internal void ChkAvailability(Guid correlationId)
    {
        RaiseEvent(new BeerAvailabilityChecked(new BeerId(Id.Value), correlationId, _beerName, _availability));
    }
    
    private void Apply(BeerAvailabilityChecked @event)
    {
        // do nothing;
    }
    #endregion
}