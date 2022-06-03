using BrewUp.Production.ReadModel.Abstracts;
using BrewUp.Production.ReadModel.MongoDb.Repository;
using BrewUp.Production.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;

namespace BrewUp.Production.ReadModel.MongoDb
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

            services.AddSingleton<IEventStorePositionRepository>(x =>
                new EventStorePositionRepository(x.GetService<ILoggerFactory>(), mongoDbParameter));

            return services;
        }
    }
}