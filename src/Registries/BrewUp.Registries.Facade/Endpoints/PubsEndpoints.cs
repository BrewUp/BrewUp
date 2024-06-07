using BrewUp.Registries.Facade.BindingModels;
using BrewUp.Registries.Facade.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BrewUp.Registries.Facade.Endpoints;

public static class PubsEndpoints
{
    public static async Task<IResult> HandleCreatePub(
        IRegistriesFacade registriesFacade,
        IValidator<PubModel> validator,
        ValidationHandler validationHandler,
        PubModel body,
        CancellationToken cancellationToken)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        var pubId = await registriesFacade.CreatePubsAsync(body, cancellationToken);

        return Results.Created($"/v1/registries/pubs/{pubId}", pubId);
    }
    
    public static async Task<IResult> HandleGetPubs(
        IRegistriesFacade registriesFacade,
        CancellationToken cancellationToken)
    {
        var pubs = await registriesFacade.GetPubsAsync(cancellationToken);

        return Results.Ok(pubs.Results);
    }
}