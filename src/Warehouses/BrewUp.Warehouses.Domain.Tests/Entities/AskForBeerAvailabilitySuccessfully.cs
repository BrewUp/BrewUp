using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Dtos;
using BrewUp.Shared.Messages.Sagas;
using BrewUp.Warehouses.Domain.CommandHandlers;
using BrewUp.Warehouses.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace BrewUp.Warehouses.Domain.Tests.Entities;

public sealed class AskForBeerAvailabilitySuccessfully : CommandSpecification<AskForBeerAvailability>
{
    private readonly Guid _correlationId = Guid.NewGuid();
    
    private readonly BeerId _beerId = new(Guid.NewGuid());
    private readonly BeerName _beerName = new ("Muflone IPA");
    private readonly Availability _availability = new (10, "Lt");
    
    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BeerAvailabilityCreated(_beerId, _beerName, _availability);
        yield return new BeerAvailabilityLoaded(_beerId, _correlationId, _beerName, _availability);
    }

    protected override AskForBeerAvailability When()
    {
        return new AskForBeerAvailability(_beerId, _correlationId, _beerName);
    }

    protected override ICommandHandlerAsync<AskForBeerAvailability> OnHandler()
    {
        return new AskForBeerAvailabilityCommandHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BeerAvailabilityChecked(_beerId, _correlationId, _beerName, _availability);
    }
}