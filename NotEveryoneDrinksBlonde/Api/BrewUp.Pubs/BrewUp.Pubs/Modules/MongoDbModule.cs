using BrewUp.Pubs.ReadModel.MongoDb;
using BrewUp.Pubs.Shared.Configuration;

namespace BrewUp.Pubs.Modules;

public class MongoDbModule : IModule
{
    public bool IsEnabled { get; } = true;
    public int Order { get; } = 1;
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        var mongoDbParameters = new MongoDbParameters();
        builder.Configuration.GetSection("BrewUp:MongoDbParameters").Bind(mongoDbParameters);
        builder.Services.AddMongoDb(mongoDbParameters);
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}