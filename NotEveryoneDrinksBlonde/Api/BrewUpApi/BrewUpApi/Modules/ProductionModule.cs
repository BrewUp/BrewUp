﻿using BrewUpApi.Modules.Production;
using BrewUpApi.Modules.Production.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BrewUpApi.Modules;

public sealed class ProductionModule : IModule
{
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddProduction();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("HelloProduction", async ([FromServices] IProductionService productionService) => await productionService.SayHelloAsync())
            .WithName("SayHelloFromProduction")
            .WithTags("Production");

        return endpoints;
    }
}