namespace BrewUpWasm.Modules.Pubs.Extensions.JsonModel;

public class PubsStorageJson
{
    public string PubId { get; set; } = string.Empty;
    public string PubName { get; set; } = string.Empty;

    public IEnumerable<StorageJson> Storage { get; set; } = Enumerable.Empty<StorageJson>();
}