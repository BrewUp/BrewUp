using BrewUp.Pubs.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Muflone;

namespace BrewUp.Pubs.Shared;

public static class SharedHelper
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        //services.AddScoped<IServiceBus, InProcessServiceBus>();
        services.AddScoped<IServiceBus, ServiceBus>();

        return services;
    }
}