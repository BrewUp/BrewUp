namespace BrewUpApi.Modules
{
    public interface IModule
    {
        IServiceCollection RegisterModule(WebApplicationBuilder builder);
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}
