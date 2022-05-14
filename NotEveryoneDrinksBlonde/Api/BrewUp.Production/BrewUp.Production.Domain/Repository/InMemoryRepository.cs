using Muflone;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BrewUp.Production.Domain.Repository;

public class InMemoryRepository : IRepository
{
    public IEnumerable<DomainEvent> Events { get; private set; } = Enumerable.Empty<DomainEvent>();

    public async Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : class, IAggregate
    {
        return await GetByIdAsync<TAggregate>(id, 0);
    }

    public Task<TAggregate> GetByIdAsync<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate
    {
        var aggregate = ConstructAggregate<TAggregate>();
        Events.ForEach(aggregate.ApplyEvent);

        return Task.FromResult(aggregate);
    }

    public async Task SaveAsync(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders)
    {
        Events = aggregate.GetUncommittedEvents().Cast<DomainEvent>();

        //return Task.CompletedTask;
    }

    public async Task SaveAsync(IAggregate aggregate, Guid commitId)
    {
        await SaveAsync(aggregate, commitId, d => { });
    }

    private static TAggregate ConstructAggregate<TAggregate>()
    {
        return (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);
    }

    #region Dispose
    public void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~InMemoryRepository()
    {
        Dispose(false);
    }
    #endregion
}

public static class Helpers
{
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        if (items == null)
            return;
        foreach (T obj in items)
            action(obj);
    }
}