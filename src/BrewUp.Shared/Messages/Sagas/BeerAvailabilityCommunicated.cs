using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Sagas;

public sealed class BeerAvailabilityCommunicated : IntegrationEvent
{
    public readonly BeerId BeerId;
    public readonly BeerName BeerName;
    public readonly Availability Availability;

    public BeerAvailabilityCommunicated(BeerId aggregateId, Guid correlationId, BeerName beerName,
        Availability availability) : base(aggregateId, correlationId)
    {
        BeerId = aggregateId;

        BeerName = beerName;
        Availability = availability;
    }
}