using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Sales.Messages.Events;

public sealed class SalesBeerAvailabilityCreated : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BeerName BeerName;
    public readonly Availability Availability;
    
    public SalesBeerAvailabilityCreated(BeerId aggregateId, BeerName beerName, Availability availability) : base(aggregateId)
    {
        BeerId = aggregateId;
        BeerName = beerName;
        Availability = availability;
    }
}