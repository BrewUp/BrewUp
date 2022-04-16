using Microsoft.OpenApi.Models;

namespace BrewUpApi.Modules;

public sealed class SwaggerModule : IModule
{
    public bool IsEnabled { get; }
    public int Order { get; }

    public SwaggerModule()
    {
        IsEnabled = true;
        Order = 0;
    }

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
        {
            Description = "BrewUp API",
            Title = "BrewUp Api - OnePintQuenchesEveryOne",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Name = "BrewUp.Api"
            }
        }));

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}