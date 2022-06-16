using BrewUpApi.Abstracts;
using BrewUpApi.Concretes;

namespace BrewUpApi.Modules;

public sealed class ProductionModule : IModule
{
    public bool IsEnabled { get; }
    public int Order { get; }

    public ProductionModule()
    {
        IsEnabled = true;
        Order = 0;
    }

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductionService, ProductionService>();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("HelloProduction", HandleGetHello)
            .WithName("SayHelloFromProduction")
            .WithTags("Production");

        return endpoints;
    }

    private async Task<IResult> HandleGetHello(IProductionService productionService)
    {
        return Results.Ok();
    }
}