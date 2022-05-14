namespace BrewUp.Pubs.Shared.Configuration;

public class ApiSettings
{
    public MongoDbParameters MongoDbParameters { get; set; } = new ();
    public EventStoreParameters EventStoreParameters { get; set; } = new ();
}

public class MongoDbParameters
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string LogConnectionString { get; set; } = string.Empty;
    public string DatabaseLogName { get; set; } = string.Empty;
}

public class EventStoreParameters
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string EventTypeHeader { get; set; } = string.Empty;
    public string AggregateTypeHeader { get; set; } = string.Empty;
}