namespace BrewUpApi.Modules;

public sealed class CorsModule : IModule
{
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", corsBuilder =>
                corsBuilder.AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader());
        });

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}