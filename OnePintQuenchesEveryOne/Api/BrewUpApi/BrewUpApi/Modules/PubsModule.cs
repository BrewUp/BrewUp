using BrewUpApi.Abstracts;
using BrewUpApi.Concretes;

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
        builder.Services.AddScoped<IPubsService, PubsService>();

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