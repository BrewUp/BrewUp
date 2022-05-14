using BrewUp.Production.Module.Extensions.JsonModel;
using FluentValidation;

namespace BrewUp.Production.Module.Validators;

public class BrewBeerValidator : AbstractValidator<BeersJson>
{
    public BrewBeerValidator()
    {
        RuleFor(v => v.BeerType).NotEmpty();
        RuleFor(v => v.Quantity).GreaterThan(0);
    }
}