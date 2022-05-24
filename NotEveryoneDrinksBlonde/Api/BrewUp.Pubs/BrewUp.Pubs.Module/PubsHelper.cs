using BrewUp.Pubs.Domain.Repository;
using BrewUp.Pubs.Messages.Events;
using BrewUp.Pubs.Module.Abstracts;
using BrewUp.Pubs.Module.Concretes;
using BrewUp.Pubs.Module.Factories;
using BrewUp.Pubs.Module.Handlers;
using BrewUp.Pubs.Module.Mapper;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Azure.Factories;
using Muflone.Factories;
using Muflone.Messages;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BrewUp.Pubs.Module;

public static class ProductionHelper
{
    public static IServiceCollection AddPubs(this IServiceCollection services)
    {
        services.AddScoped<IPubsService, PubsService>();
        services.AddScoped<IPubsStorageService, PubsStorageService>();
        services.AddScoped<IRepository, InMemoryRepository>();

        services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<PubsService>());

        services.AddScoped<IMessageMapperFactory, MessageMapperFactory>();
        services.AddScoped<IMessageMapper<BeerBrewed>, BeerBrewedMapper>();

        services.AddScoped<ICommandProcessorFactoryAsync, CommandProcessorFactoryAsync>();

        services.AddScoped<IDomainEventHandlerFactoryAsync, DomainEventHandlerFactory>();
        services.AddScoped<IDomainEventProcessorFactoryAsync, DomainEventProcessorFactoryAsync>();
        services.AddScoped<IDomainEventHandlerAsync<BeerBrewed>, BeerBrewedEventHandler>();
        services.AddScoped<IDomainEventHandlerAsync<BeerBrewed>, BeerBrewedForPubEventHandler>();

        return services;
    }
}