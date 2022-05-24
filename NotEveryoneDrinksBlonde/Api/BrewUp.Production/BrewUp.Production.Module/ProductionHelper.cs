using BrewUp.Production.Domain.CommandsHandler;
using BrewUp.Production.Domain.Repository;
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
using Muflone.Persistence;

namespace BrewUp.Production.Module;

public static class ProductionHelper
{
    public static IServiceCollection AddProduction(this IServiceCollection services)
    {
        services.AddScoped<IProductionService, ProductionService>();
        services.AddScoped<IRepository, InMemoryRepository>();

        services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ProductionService>());

        services.AddScoped<ICommandHandlerFactoryAsync, CommandHandlerFactoryAsync>();
        services.AddScoped<ICommandProcessorFactoryAsync, CommandProcessorFactoryAsync>();

        services.AddScoped<ICommandHandlerAsync<BrewBeer>, BrewBeerCommandHandler>();
        services.AddScoped<IDomainEventHandlerAsync<BeerBrewed>, BeerBrewedEventHandler>();

        services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactory>();
        services.AddScoped<IDomainEventProcessorFactoryAsync, DomainEventProcessorFactoryAsync>();

        return services;
    }
}