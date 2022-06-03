using BrewUp.Production.Domain.CommandsHandler;
using BrewUp.Production.Messages.Commands;
using BrewUp.Production.Messages.Events;
using BrewUp.Production.Module.Abstracts;
using BrewUp.Production.Module.Concretes;
using BrewUp.Production.Module.Factories;
using BrewUp.Production.Module.Handlers;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Azure.Factories;
using Muflone.Factories;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace BrewUp.Production.Module;

public static class ProductionHelper
{
    public static IServiceCollection AddProduction(this IServiceCollection services)
    {
        services.AddScoped<IProductionService, ProductionService>();

        services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ProductionService>());

        services.AddSingleton<ICommandHandlerFactoryAsync, CommandHandlerFactoryAsync>();
        services.AddSingleton<ICommandProcessorFactoryAsync, CommandProcessorFactoryAsync>();

        services.AddSingleton<ICommandHandlerAsync<BrewBeer>, BrewBeerCommandHandler>();
        services.AddSingleton<IDomainEventHandlerAsync<BeerBrewed>, BeerBrewedEventHandler>();

        services.AddSingleton<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactory>();
        services.AddSingleton<IDomainEventProcessorFactoryAsync, DomainEventProcessorFactoryAsync>();

        return services;
    }
}