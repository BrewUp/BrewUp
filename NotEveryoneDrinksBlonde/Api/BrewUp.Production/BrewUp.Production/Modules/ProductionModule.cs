using BrewUp.Production.Module;
using BrewUp.Production.Module.Abstracts;
using BrewUp.Production.Module.Extensions.JsonModel;
using BrewUp.Production.Shared.Services;
using FluentValidation;

namespace BrewUp.Production.Modules;

public sealed class ProductionModule : IModule
{
    public bool IsEnabled { get; } = true;
    public int Order { get; } = 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddProduction();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/production/Beers", HandlePostBrewBeer)
            .WithName("BrewBeer")
            .WithTags("Production");

        endpoints.MapGet("/production/Beers", HandleGetBeers)
            .WithName("GetBeers")
            .WithTags("Production");

        return endpoints;
    }

    private static async Task<IResult> HandleGetBeers(IProductionService productionService)
    {
        try
        {
            var beers = await productionService.GetBeersAsync();
            return Results.Ok(beers);
        }
        catch (Exception ex)
        {
            Console.WriteLine(CommonServices.GetDefaultErrorTrace(ex));
            return Results.BadRequest(CommonServices.GetErrorMessage(ex));
        }
    }

    private static async Task<IResult> HandlePostBrewBeer(BeersJson brewBeer, IProductionService productionService,
        IValidator<BeersJson> validator)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(brewBeer);
            if (validationResult.IsValid)
            {
                await productionService.PrepareBeerAsync(brewBeer);
                return Results.Accepted();
            }

            var errors = validationResult.Errors.GroupBy(e => e.PropertyName)
                .ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());
            return Results.ValidationProblem(errors);
        }
        catch (Exception ex)
        {
            Console.WriteLine(CommonServices.GetDefaultErrorTrace(ex));
            return Results.BadRequest(CommonServices.GetErrorMessage(ex));
        }
    }
}