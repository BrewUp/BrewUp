using BrewUp.Production.Module.Extensions.CustomTypes;
using BrewUp.Production.Module.Extensions.JsonModel;
using BrewUp.Production.ReadModel.Abstracts;

namespace BrewUp.Production.ReadModel.Dtos;

public class Beers : DtoBase
{
    public string BeerType { get; private set; } = string.Empty;
    public double Quantity { get; private set; }

    protected Beers()
    {}

    public static Beers CreateBeers(BeerId beerId, BeerType beerType, BeerQuantity quantity) =>
        new(beerId.ToString(), beerType.Value, quantity.Value);

    private Beers(string beerId, string beerType, double quantity)
    {
        Id = beerId;
        BeerType = beerType;
        Quantity = quantity;
    }

    public BeersJson ToJson() => new ()
    {
        Id = Id,
        BeerType = BeerType,
        Quantity = Quantity
    };
}