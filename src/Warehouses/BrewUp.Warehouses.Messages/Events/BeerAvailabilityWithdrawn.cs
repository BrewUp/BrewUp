using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.Messages.Events;

public sealed class BeerAvailabilityWithdrawn : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly BeerName BeerName;
    public readonly Availability Availability;
    
    public BeerAvailabilityWithdrawn(BeerId aggregateId, Guid commitId, BeerName beerName, Availability availability) 
        : base(aggregateId, commitId)
    {
        BeerId = aggregateId;
        BeerName = beerName;
        Availability = availability;
    }
}