using BrewUp.Production.Shared;

namespace BrewUp.Production.Modules;

public class SharedModule : IModule
{
    public bool IsEnabled { get; } = true;
    public int Order { get; } = 99;
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddSharedServices();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}