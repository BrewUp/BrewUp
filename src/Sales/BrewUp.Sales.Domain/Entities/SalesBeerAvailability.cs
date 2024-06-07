using BrewUp.Sales.Messages.Events;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Core;

namespace BrewUp.Sales.Domain.Entities;

public sealed class SalesBeerAvailability : AggregateRoot
{
    private BeerName _beerName;
    
    private Availability _availability = new Availability(0, string.Empty);
    
    protected SalesBeerAvailability()
    {
    }

    #region CreateBeerAvailability
    internal static SalesBeerAvailability CreateBeerAvailability(BeerId beerId, BeerName beerName, Availability availability)
    {
        return new SalesBeerAvailability(beerId, beerName, availability);
    }
    
    private SalesBeerAvailability(BeerId beerId, BeerName beerName, Availability availability)
    {
        RaiseEvent(new SalesBeerAvailabilityCreated(beerId, beerName, availability));
    }
    
    private void Apply(SalesBeerAvailabilityCreated @event)
    {
        Id = @event.AggregateId;
        _beerName = @event.BeerName;

        _availability = @event.Availability;
    }
    #endregion

    #region LoadAvailability
    internal void LoadAvailability(Availability availability, Guid correlationId)
    {
        RaiseEvent(new SalesBeerAvailabilityLoaded(new BeerId(Id.Value), correlationId, availability));
    }
    
    private void Apply(SalesBeerAvailabilityLoaded @event)
    {
        _availability = @event.Availability with {Value = _availability.Value + @event.Availability.Value};
    }
    #endregion
}