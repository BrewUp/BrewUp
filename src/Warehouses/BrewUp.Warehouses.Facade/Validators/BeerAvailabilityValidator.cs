using BrewUp.Shared.Contracts;
using FluentValidation;

namespace BrewUp.Warehouses.Facade.Validators;

public sealed class BeerAvailabilityValidator : AbstractValidator<BeerAvailabilityJson>
{
    public BeerAvailabilityValidator()
    {
        RuleFor(v => v.BeerId).NotEmpty();
        RuleFor(v => v.BeerName).NotEmpty();
    }
}