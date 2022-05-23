using BrewUp.Pubs.Module.Extensions.CustomTypes;
using BrewUp.Pubs.Module.Extensions.JsonModel;
using BrewUp.Pubs.ReadModel.Abstracts;

namespace BrewUp.Pubs.ReadModel.Dtos;

public class Beers : DtoBase
{
    public string BeerType { get; private set; } = string.Empty;
    public int Quantity { get; private set; }

    protected Beers()
    {}

    public static Beers CreateBeers(BeerId beerId, BeerType beerType, BeerQuantity quantity) =>
        new(beerId.ToString(), beerType.Value, quantity.Value);

    private Beers(string beerId, string beerType, int quantity)
    {
        Id = beerId;
        BeerType = beerType;
        Quantity = quantity;
    }

    public BeersJson ToJson() => new ()
    {
        BeerId = Id,
        BeerType = BeerType,
        Quantity = Quantity
    };
}