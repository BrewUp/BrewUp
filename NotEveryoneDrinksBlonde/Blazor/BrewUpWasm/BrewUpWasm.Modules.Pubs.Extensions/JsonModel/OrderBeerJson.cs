namespace BrewUpWasm.Modules.Pubs.Extensions.JsonModel;

public class OrderBeerJson
{
    public string PubId { get; set; } = string.Empty;
    public string PubName { get; set; } = string.Empty;

    public string BeerType { get; set; } = string.Empty;
    public int BottleQuantity { get; set; } = 0;
}