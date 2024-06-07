using BrewUp.Shared.Contracts;
using BrewUp.Warehouses.Facade.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BrewUp.Warehouses.Facade.Endpoints;

public static class WarehousesEndpoint
{
    public static async Task<IResult> HandleLoadAvailability(
        IWarehousesFacade warehousesFacade,
        IValidator<BeerAvailabilityJson> validator,
        ValidationHandler validationHandler,
        BeerAvailabilityJson body,
        CancellationToken cancellationToken)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        await warehousesFacade.LoadAvailabilityAsync(body, cancellationToken);

        return Results.Ok();
    }
}