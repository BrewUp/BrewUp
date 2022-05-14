using BrewUp.Production.Domain.Repository;
using BrewUp.Production.Shared.Abstracts;
using BrewUp.Production.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Muflone.Persistence;

namespace BrewUp.Production.Mediator;

public static class MediatorHelper
{
    public static IServiceCollection StartBroker(this IServiceCollection services)
    {
        services.AddScoped<IRegisterHandler, RegisterHandlers>();
        services.AddScoped<IPublish, Publish>();
        services.AddScoped<IRepository, InMemoryRepository>();

        services.AddSingleton<IHostedService>(new StartEventsSubscriber(services));

        return services;
    }
}