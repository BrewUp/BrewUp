using System.Linq.Expressions;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using BrewUp.Warehouses.ReadModel.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BrewUp.Warehouses.ReadModel.Queries;

public sealed class BeerAvailabilityQueries : IQueries<BeerAvailability>
{
    private readonly IMongoClient _mongoClient;
    private IMongoDatabase _database;
    
    public string DatabaseName { get; private set; }

    public BeerAvailabilityQueries(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        SetDatabaseName("BrewUp_Warehouses");
    }
    
    public void SetDatabaseName(string databaseName)
    {
        DatabaseName = databaseName;
        _database = _mongoClient.GetDatabase(databaseName);
    }

    public async Task<BeerAvailability> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<BeerAvailability>(nameof(BeerAvailability));
        var filter = Builders<BeerAvailability>.Filter.Eq("_id", id);
        return (await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0
            ? (await collection.FindAsync(filter, cancellationToken: cancellationToken)).First()
            : null)!;
    }

    public async Task<PagedResult<BeerAvailability>> GetByFilterAsync(Expression<Func<BeerAvailability, bool>>? query, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        if (--page < 0)
            page = 0;

        var collection = _database.GetCollection<BeerAvailability>(nameof(BeerAvailability));
        var queryable = query != null
            ? collection.AsQueryable().Where(query)
            : collection.AsQueryable();

        var count = await queryable.CountAsync(cancellationToken: cancellationToken);
        var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

        return new PagedResult<BeerAvailability>(results, page, pageSize, count);
    }
}