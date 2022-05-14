using Muflone.Messages.Events;

namespace BrewUp.Pubs.Shared.Abstracts;

public interface IPublish
{
    Task PublishAsync<T>(T @event) where T : IDomainEvent;
}