using BrewUp.Production.ReadModel.Dtos;
using BrewUp.Production.Shared.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone.Eventstore;
using Muflone.Eventstore.Persistence;

namespace BrewUp.Production.ReadModel.MongoDb.Repository;

public class EventStorePositionRepository : IEventStorePositionRepository
{
    private readonly IMongoDatabase _database;
    private readonly ILogger _logger;

    public EventStorePositionRepository(ILoggerFactory loggerFactory, MongoDbParameters mongoDbParameters)
    {
        _logger = loggerFactory.CreateLogger(GetType());

        var client = new MongoClient(mongoDbParameters.ConnectionString);
        _database = client.GetDatabase(mongoDbParameters.DatabaseName);
    }

    public async Task<IEventStorePosition> GetLastPosition()
    {
        try
        {
            var collection = _database.GetCollection<LastEventPosition>(typeof(LastEventPosition).Name);
            var filter = Builders<LastEventPosition>.Filter.Eq("_id", Constants.LastEventPositionKey);
            var result = await collection.CountDocumentsAsync(filter) > 0
                ? (await collection.FindAsync(filter)).First()
                : null;

            if (result is not null)
                return new EventStorePosition(result.CommitPosition, result.PreparePosition);

            result = new LastEventPosition(Constants.LastEventPositionKey, -1, -1);
            await collection.InsertOneAsync(result);

            return new EventStorePosition(result.CommitPosition, result.PreparePosition);
        }
        catch (Exception ex)
        {
            _logger.LogError($"EventStorePositionRepository: Error getting LastSavedPostion, Message: {ex.Message}, StackTrace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task Save(IEventStorePosition position)
    {
        try
        {
            var collection = _database.GetCollection<LastEventPosition>(typeof(LastEventPosition).Name);
            var filter = Builders<LastEventPosition>.Filter.Eq("_id", Constants.LastEventPositionKey);
            var entity = await collection.Find(filter).FirstOrDefaultAsync();

            if (entity is null)
            {
                entity = new LastEventPosition(Constants.LastEventPositionKey, position.CommitPosition, position.PreparePosition);
                await collection.InsertOneAsync(entity);
            }
            else
            {
                if (position.CommitPosition > entity.CommitPosition && position.PreparePosition > entity.PreparePosition)
                {
                    entity.CommitPosition = position.CommitPosition;
                    entity.PreparePosition = position.PreparePosition;
                    await collection.FindOneAndReplaceAsync(filter, entity);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"EventStorePositionRepository: Error while updating commit position: {e.Message}, StackTrace: {e.StackTrace}");
            throw;
        }
    }
}