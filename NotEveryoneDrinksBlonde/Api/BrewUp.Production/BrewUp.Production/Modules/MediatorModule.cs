using BrewUp.Production.Mediator;

namespace BrewUp.Production.Modules;

public class MediatorModule : IModule
{
    public bool IsEnabled { get; } = true;
    public int Order { get; } = 99;
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddCommandProcessor(builder.Configuration["BrewUp:ServiceBus:ConnectionString"]);
        builder.Services.AddDomainProcessor(builder.Configuration["BrewUp:ServiceBus:ConnectionString"]);
        builder.Services.StartBroker();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}