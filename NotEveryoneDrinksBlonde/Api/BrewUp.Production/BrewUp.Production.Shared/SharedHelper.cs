using BrewUp.Production.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Muflone;

namespace BrewUp.Production.Shared;

public static class SharedHelper
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterHandler, RegisterHandlers>();

        services.AddSingleton<IServiceBus, ServiceBus>();
        services.AddSingleton<IEventBus, ServiceBus>();

        services.AddSingleton<IHostedService>(new StartSubscriptions(services));

        return services;
    }
}