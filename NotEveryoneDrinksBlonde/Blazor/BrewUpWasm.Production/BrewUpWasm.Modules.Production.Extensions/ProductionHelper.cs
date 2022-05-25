using BrewUpWasm.Modules.Production.Extensions.Abstracts;
using BrewUpWasm.Modules.Production.Extensions.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpWasm.Modules.Production.Extensions;

public static class ProductionHelper
{
    public static IServiceCollection AddProductionServices(this IServiceCollection services)
    {
        services.AddScoped<IProductionService, ProductionService>();

        return services;
    }
}