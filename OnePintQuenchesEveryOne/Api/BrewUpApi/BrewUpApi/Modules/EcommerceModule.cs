namespace BrewUpApi.Modules;

public sealed class EcommerceModule : IModule
{
    public bool IsEnabled { get; }
    public int Order { get; }

    public EcommerceModule()
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
        endpoints.MapGet("HelloECommerce", () => "Hello from eCommerce")
            .WithName("SayHelloFromECommerce")
            .WithTags("eCommerce");

        return endpoints;
    }
}