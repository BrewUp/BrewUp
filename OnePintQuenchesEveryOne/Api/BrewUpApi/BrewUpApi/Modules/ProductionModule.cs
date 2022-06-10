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
        endpoints.MapGet("HelloProduction", () => "Hello from Production")
            .WithName("SayHelloFromProduction")
            .WithTags("Production");

        return endpoints;
    }
}