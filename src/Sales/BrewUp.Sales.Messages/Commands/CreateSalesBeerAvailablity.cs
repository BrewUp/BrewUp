using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Commands;

namespace BrewUp.Sales.Messages.Commands;

public sealed class CreateSalesBeerAvailablity : Command
{
    public readonly BeerId BeerId;
    public readonly BeerName BeerName;
    public readonly Availability Availability;
    
    public CreateSalesBeerAvailablity(BeerId aggregateId, BeerName beerName, Availability availability) : base(aggregateId)
    {
        BeerId = aggregateId;
        BeerName = beerName;
        Availability = availability;
    }
}