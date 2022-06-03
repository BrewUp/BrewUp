using Muflone.Eventstore;

namespace BrewUp.Production.Modules;

public class EventStoreModule : IModule
{
    public bool IsEnabled { get; } = true;
    public int Order { get; } = 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddMufloneEventStore(builder.Configuration["BrewUp:EventStoreParameters:ConnectionString"]);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}