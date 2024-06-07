using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Commands;

namespace BrewUp.Warehouses.Messages.Commands;

public sealed class WithdrawalBySalesOrder : Command
{
    public readonly BeerId BeerId;
    public readonly Quantity Quantity;
    
    public WithdrawalBySalesOrder(BeerId aggregateId, Guid commitId, Quantity quantity) 
        : base(aggregateId, commitId)
    {
        BeerId = aggregateId;
        Quantity = quantity;
    }
}