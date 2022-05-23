using BrewUp.Production.Module;
using BrewUp.Production.Module.Abstracts;
using BrewUp.Production.Shared.Services;

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
}