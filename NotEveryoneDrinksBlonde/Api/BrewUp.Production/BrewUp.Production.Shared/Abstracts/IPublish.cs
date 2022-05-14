using Muflone.Messages.Events;

namespace BrewUp.Production.Shared.Abstracts;

public interface IPublish
{
    Task PublishAsync<T>(T @event) where T : IDomainEvent;
}