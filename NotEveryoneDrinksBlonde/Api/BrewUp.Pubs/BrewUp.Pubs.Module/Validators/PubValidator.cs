using BrewUp.Pubs.Module.Extensions.JsonModel;
using FluentValidation;

namespace BrewUp.Pubs.Module.Validators;

public class PubValidator : AbstractValidator<PubJson>
{
    public PubValidator()
    {
        RuleFor(v => v.PubName).NotEmpty();
    }
}