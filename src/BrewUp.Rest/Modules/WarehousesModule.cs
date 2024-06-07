using BrewUp.Warehouses.Facade;
using BrewUp.Warehouses.Facade.Endpoints;

namespace BrewUp.Rest.Modules;

public sealed class WarehousesModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddWarehouses();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/v1/warehouses/")
            .WithTags("Warehouses");
        
        group.MapPost("/availability", WarehousesEndpoint.HandleLoadAvailability)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .WithName("LoadAvailability");

        return endpoints;
    }
}