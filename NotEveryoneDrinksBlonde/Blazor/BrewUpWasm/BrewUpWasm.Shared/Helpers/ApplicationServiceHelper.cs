using BrewUpWasm.Shared.Abstracts;
using BrewUpWasm.Shared.Concretes;
using BrewUpWasm.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpWasm.Shared.Helpers
{
    public static class ApplicationServiceHelper
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<ToastService>();

            services.AddSingleton<AppState>();

            return services;
        }
    }
}