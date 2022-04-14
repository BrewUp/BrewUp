namespace BrewUpApi.Modules;

public sealed class ProductionModule : IModule
{
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
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