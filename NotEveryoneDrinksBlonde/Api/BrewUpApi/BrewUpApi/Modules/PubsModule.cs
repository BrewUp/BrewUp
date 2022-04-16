namespace BrewUpApi.Modules;

public sealed class PubsModule : IModule
{
    public bool IsEnabled { get; }
    public int Order { get; }

    public PubsModule()
    {
        IsEnabled = true;
        Order = 0;
    }

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