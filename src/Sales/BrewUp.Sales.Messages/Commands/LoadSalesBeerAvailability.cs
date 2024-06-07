using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Commands;

namespace BrewUp.Sales.Messages.Commands;

public sealed class LoadSalesBeerAvailability : Command
{
    public readonly BeerId BeerId;
    public readonly Availability Availability;
    
    public LoadSalesBeerAvailability(BeerId aggregateId, Availability availability) : base(aggregateId)
    {
        BeerId = aggregateId;
        Availability = availability;
    }
}