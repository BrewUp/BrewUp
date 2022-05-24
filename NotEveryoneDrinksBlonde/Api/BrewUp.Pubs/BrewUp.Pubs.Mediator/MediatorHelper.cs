using BrewUp.Pubs.Shared.Abstracts;
using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BrewUp.Pubs.Mediator;

public static class MediatorHelper
{
    public static IServiceCollection StartBroker(this IServiceCollection services)
    {
        services.AddScoped<IRegisterHandler, RegisterHandlers>();
        services.AddScoped<IPublish, Publish>();

        services.AddSingleton<IHostedService>(new StartEventsSubscriber(services));

        return services;
    }
}