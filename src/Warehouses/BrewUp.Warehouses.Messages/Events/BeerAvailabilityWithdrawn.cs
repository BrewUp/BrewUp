using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.Messages.Events;

public sealed class BeerAvailabilityWithdrawn : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly Availability Availability;
    
    public BeerAvailabilityWithdrawn(BeerId aggregateId, Guid commitId, Availability availability) 
        : base(aggregateId, commitId)
    {
        BeerId = aggregateId;
        Availability = availability;
    }
}