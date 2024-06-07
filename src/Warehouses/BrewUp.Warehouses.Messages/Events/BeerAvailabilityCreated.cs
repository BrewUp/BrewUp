using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.Messages.Events;

public sealed class BeerAvailabilityCreated : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BeerName BeerName;
    public readonly Availability Availability;
    
    public BeerAvailabilityCreated(BeerId aggregateId, BeerName beerName, Availability availability) : base(aggregateId)
    {
        BeerId = aggregateId;
        BeerName = beerName;
        Availability = availability;
    }
}