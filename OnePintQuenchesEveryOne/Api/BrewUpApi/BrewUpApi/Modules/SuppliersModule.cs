namespace BrewUpApi.Modules;

public sealed class SuppliersModule : IModule
{
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("HelloSuppliers", () => "Hello from Suppliers")
            .WithName("SayHelloFromSuppliers")
            .WithTags("Suppliers");

        return endpoints;
    }
}