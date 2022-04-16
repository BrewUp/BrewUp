namespace BrewUpApi.Modules;

public sealed class PubsModule : IModule
{
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("HelloPubs", () => "Hello from Pubs")
            .WithName("SayHelloFromPubs")
            .WithTags("Pubs");

        return endpoints;
    }
}