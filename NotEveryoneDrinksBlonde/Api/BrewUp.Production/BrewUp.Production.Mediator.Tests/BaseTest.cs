using System;
using BrewUp.Production.Module;
using BrewUp.Production.ReadModel.MongoDb;
using BrewUp.Production.Shared;
using BrewUp.Production.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Production.Mediator.Tests;

public abstract class BaseTest
{
    protected readonly IServiceProvider ServiceProvider;

    protected BaseTest()
    {
        var services = new ServiceCollection();

        services.AddSingleton(typeof(IServiceProvider)).AddLogging();
        services.AddSharedServices();

        services.AddMongoDb(new MongoDbParameters
        {
            ConnectionString = "mongodb://localhost",
            DatabaseName = "BrewUp-Production"
        });
        services.AddProduction();

        services.AddCommandProcessor("Endpoint=sb://brewup.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=gHZkmA68xqKi5dHqB4231PwZEuEh0jptNiBDqkOdvSQ=");
        services.AddDomainProcessor(
            "Endpoint=sb://brewup.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=gHZkmA68xqKi5dHqB4231PwZEuEh0jptNiBDqkOdvSQ=");

        ServiceProvider = services.BuildServiceProvider();
    }
}