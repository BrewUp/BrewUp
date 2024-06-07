using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.Messages.Events;

public sealed class BeerAvailabilityChecked : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BeerName BeerName;
    public readonly Availability Availability;
    
    public BeerAvailabilityChecked(BeerId aggregateId, Guid correlationId, BeerName beerName, Availability availability)
        : base(aggregateId, correlationId)
    {
        BeerId = aggregateId;

        BeerName = beerName;
        Availability = availability;
    }
}