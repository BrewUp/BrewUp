using BrewUp.Production.Module.Abstracts;
using BrewUp.Production.Module.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Production.Module;

public static class ProductionHelper
{
    public static IServiceCollection AddProduction(this IServiceCollection services)
    {
        services.AddScoped<IProductionService, ProductionService>();

        return services;
    }
}