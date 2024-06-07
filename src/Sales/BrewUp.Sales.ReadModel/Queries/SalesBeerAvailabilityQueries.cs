using System.Linq.Expressions;
using BrewUp.Sales.ReadModel.Entities;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BrewUp.Sales.ReadModel.Queries;

public sealed class SalesBeerAvailabilityQueries : IQueries<SalesBeerAvailability>
{
    private readonly IMongoClient _mongoClient;
    private IMongoDatabase _database;
    
    public string DatabaseName { get; private set; }

    public SalesBeerAvailabilityQueries(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        SetDatabaseName("BrewUp_Sales");
    }
    
    public void SetDatabaseName(string databaseName)
    {
        DatabaseName = databaseName;
        _database = _mongoClient.GetDatabase(databaseName);
    }

    public async Task<SalesBeerAvailability> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<SalesBeerAvailability>(nameof(SalesBeerAvailability));
        var filter = Builders<SalesBeerAvailability>.Filter.Eq("_id", id);
        return (await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0
            ? (await collection.FindAsync(filter, cancellationToken: cancellationToken)).First()
            : null)!;
    }

    public async Task<PagedResult<SalesBeerAvailability>> GetByFilterAsync(Expression<Func<SalesBeerAvailability, bool>>? query, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        if (--page < 0)
            page = 0;

        var collection = _database.GetCollection<SalesBeerAvailability>(nameof(SalesBeerAvailability));
        var queryable = query != null
            ? collection.AsQueryable().Where(query)
            : collection.AsQueryable();

        var count = await queryable.CountAsync(cancellationToken: cancellationToken);
        var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

        return new PagedResult<SalesBeerAvailability>(results, page, pageSize, count);
    }
}