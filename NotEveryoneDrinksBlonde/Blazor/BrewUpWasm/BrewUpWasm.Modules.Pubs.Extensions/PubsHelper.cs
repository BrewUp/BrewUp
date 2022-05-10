using BrewUpWasm.Modules.Pubs.Extensions.Abstracts;
using BrewUpWasm.Modules.Pubs.Extensions.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpWasm.Modules.Pubs.Extensions;

public static class PubsHelper
{
    public static IServiceCollection AddPubs(this IServiceCollection services)
    {
        services.AddScoped<IPubsService, PubsService>();

        return services;
    }
}