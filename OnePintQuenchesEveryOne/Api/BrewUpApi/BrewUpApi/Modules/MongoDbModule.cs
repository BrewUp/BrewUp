using BrewUp.Modules.Logistics.ReadModel.MongoDb;
using BrewUpApi.Shared.Configuration;

namespace BrewUpApi.Modules;

public class MongoDbModule : IModule
{
    public bool IsEnabled { get; } = true;
    public int Order { get; } = 0;
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        var mongoDbSettings = new MongoDbParameters();
        builder.Services.AddMongoDb(mongoDbSettings);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}