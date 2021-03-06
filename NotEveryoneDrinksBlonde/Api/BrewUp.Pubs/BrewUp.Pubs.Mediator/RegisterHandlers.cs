using BrewUp.Pubs.Messages.Events;
using Muflone.Azure.Abstracts;
using Muflone.Azure.Subscriptions;

namespace BrewUp.Pubs.Mediator;

public class RegisterHandlers : IRegisterHandler
{
    private readonly IRegisterHandlersAsync _registerHandlersAsync;

    public RegisterHandlers(IServiceProvider provider)
    {
        _registerHandlersAsync = new RegisterHandlersAsync(provider);
    }

    public void RegisterDomainEventHandlers()
    {
        _registerHandlersAsync.RegisterDomainEventHandler<BeerBrewed>();
    }
}