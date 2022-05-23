namespace BrewUpWasm.Modules.Pubs.Extensions.JsonModel;

public class BeerJson
{
    public string PubId { get; set; } = string.Empty;
    public string PubName { get; set; } = string.Empty;

    public string BeerId { get; set; } = string.Empty;
    public string BeerType { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
}