using BrewUpWasm.Production.Shared.Abstracts;
using BrewUpWasm.Production.Shared.Concretes;
using BrewUpWasm.Production.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpWasm.Production.Shared.Helpers
{
    public static class ApplicationServiceHelper
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();

            services.AddScoped<ToastService>();

            services.AddSingleton<AppState>();

            return services;
        }
    }
}