using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Muflone;

namespace BrewUp.Pubs.Shared;

public static class SharedHelper
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterHandler, RegisterHandlers>();

        services.AddSingleton<IServiceBus, ServiceBus>();
        services.AddSingleton<IEventBus, ServiceBus>();

        services.AddSingleton<IHostedService>(new StartEventsSubscriber(services));

        return services;
    }
}