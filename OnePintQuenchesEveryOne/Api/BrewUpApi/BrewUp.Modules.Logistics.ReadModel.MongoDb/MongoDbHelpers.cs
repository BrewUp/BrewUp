using BrewUp.Modules.Logistics.ReadModel.Abstracts;
using BrewUp.Modules.Logistics.ReadModel.MongoDb.Repository;
using BrewUpApi.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BrewUp.Modules.Logistics.ReadModel.MongoDb
{
    public static class MongoDbHelpers
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, MongoDbParameters mongoDbParameter)
        {
            services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoDbParameter.ConnectionString));
            services.AddScoped(provider =>
                provider.GetService<IMongoClient>()
                    ?.GetDatabase(mongoDbParameter.DatabaseName)
                    .WithWriteConcern(WriteConcern.W1));

            services.AddScoped<IPersister, Persister>();

            return services;
        }
    }
}