using BrewUp.Pubs.Module;
using BrewUp.Pubs.Module.Abstracts;
using BrewUp.Pubs.Module.Extensions.JsonModel;
using BrewUp.Pubs.Shared.Services;
using FluentValidation;

namespace BrewUp.Pubs.Modules;

public sealed class PubsModule : IModule
{
    public bool IsEnabled { get; } = true;
    public int Order { get; } = 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddPubs();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/pubs/Beers", HandlePostBrewBeer)
            .WithName("BrewBeer")
            .WithTags("Pubs");

        endpoints.MapGet("/pubs/Beers", HandleGetBeers)
            .WithName("GetBeers")
            .WithTags("Pubs");

        return endpoints;
    }

    private static async Task<IResult> HandleGetBeers(IPubsService pubsService)
    {
        try
        {
            var beers = await pubsService.GetBeersAsync();
            return Results.Ok(beers);
        }
        catch (Exception ex)
        {
            Console.WriteLine(CommonServices.GetDefaultErrorTrace(ex));
            return Results.BadRequest(CommonServices.GetErrorMessage(ex));
        }
    }

    private static async Task<IResult> HandlePostBrewBeer(BeersJson brewBeer, IPubsService pubsService,
        IValidator<BeersJson> validator)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(brewBeer);
            if (validationResult.IsValid)
            {
                await pubsService.PrepareBeerAsync(brewBeer);
                return Results.Accepted();
            }

            var errors = validationResult.Errors.GroupBy(e => e.PropertyName)
                .ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());
            return Results.ValidationProblem(errors);
        }
        catch (Exception ex)
        {
            Console.WriteLine(CommonServices.GetDefaultErrorTrace(ex));
            return Results.BadRequest(CommonServices.GetErrorMessage(ex));
        }
    }
}