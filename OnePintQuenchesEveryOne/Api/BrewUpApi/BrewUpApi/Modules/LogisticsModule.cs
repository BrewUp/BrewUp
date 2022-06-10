using BrewUp.Modules.Logistics;
using BrewUp.Modules.Logistics.Abstracts;
using BrewUp.Modules.Logistics.Concretes;
using BrewUpApi.Abstracts;
using BrewUpApi.Concretes;

namespace BrewUpApi.Modules;

public sealed class LogisticsModule : IModule
{
    public bool IsEnabled { get; }
    public int Order { get; }

    public LogisticsModule()
    {
        IsEnabled = true;
        Order = 0;
    }

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddLogistics();

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