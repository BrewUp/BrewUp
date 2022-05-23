using BrewUp.Pubs.Messages.Events;
using BrewUp.Pubs.Module.Abstracts;
using BrewUp.Pubs.Module.Concretes;
using BrewUp.Pubs.Module.Factories;
using BrewUp.Pubs.Module.Handlers;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Azure.Factories;
using Muflone.Factories;
using Muflone.Messages.Events;

namespace BrewUp.Pubs.Module;

public static class ProductionHelper
{
    public static IServiceCollection AddPubs(this IServiceCollection services)
    {
        services.AddScoped<IPubsService, PubsService>();
        services.AddScoped<IPubsStorageService, PubsStorageService>();

        services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<PubsService>());

        services.AddScoped<ICommandProcessorFactoryAsync, CommandProcessorFactoryAsync>();

        services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactory>();
        services.AddScoped<IDomainEventProcessorFactoryAsync, DomainEventProcessorFactoryAsync>();
        services.AddScoped<IDomainEventHandlerAsync<BeerBrewed>, BeerBrewedEventHandler>();
        services.AddScoped<IDomainEventHandlerAsync<BeerBrewed>, BeerBrewedForPubEventHandler>();

        return services;
    }
}