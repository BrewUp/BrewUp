using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Sales.Messages.Events;

public sealed class SalesBeerAvailabilityLoaded : DomainEvent
{
    public readonly BeerId BeerId;
    public readonly Availability Availability;
    
    public SalesBeerAvailabilityLoaded(BeerId aggregateId, Guid correlationId, Availability availability) : base(aggregateId, correlationId)
    {
        BeerId = aggregateId;
        Availability = availability;
    }
}