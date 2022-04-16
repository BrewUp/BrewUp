using BrewUpApi.Modules.Production.Abstracts;
using BrewUpApi.Modules.Production.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpApi.Modules.Production;

public static class ProductionHelper
{
    public static IServiceCollection AddProduction(this IServiceCollection services)
    {
        services.AddScoped<IProductionService, ProductionService>();

        return services;
    }
}