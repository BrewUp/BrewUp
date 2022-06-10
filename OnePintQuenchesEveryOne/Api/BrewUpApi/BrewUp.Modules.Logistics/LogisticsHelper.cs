using BrewUp.Modules.Logistics.Abstracts;
using BrewUp.Modules.Logistics.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Modules.Logistics;

public static class LogisticsHelper
{
    public static IServiceCollection AddLogistics(this IServiceCollection services)
    {
        services.AddScoped<ILogisticsService, LogisticsService>();

        return services;
    }

}