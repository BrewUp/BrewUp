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