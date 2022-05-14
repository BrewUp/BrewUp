using BrewUp.Pubs.Module.Extensions.JsonModel;
using FluentValidation;

namespace BrewUp.Pubs.Module.Validators;

public class BrewBeerValidator : AbstractValidator<BeersJson>
{
    public BrewBeerValidator()
    {
        RuleFor(v => v.BeerType).NotEmpty();
        RuleFor(v => v.Quantity).GreaterThan(0);
    }
}