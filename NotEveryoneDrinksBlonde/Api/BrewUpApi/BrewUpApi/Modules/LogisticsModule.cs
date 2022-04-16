namespace BrewUpApi.Modules;

public sealed class LogisticsModule : IModule
{
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("HelloLogistics", () => "Hello from Logistics")
            .WithName("SayHelloFromLogistics")
            .WithTags("Logistics");

        return endpoints;
    }
}