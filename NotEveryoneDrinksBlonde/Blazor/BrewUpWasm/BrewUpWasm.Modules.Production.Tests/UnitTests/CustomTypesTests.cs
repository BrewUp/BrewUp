using BrewUpWasm.Modules.Production.Extensions.CustomTypes;
using Xunit;

namespace BrewUpWasm.Modules.Production.Tests.UnitTests;

public class CustomTypesTests
{
    [Fact]
    public void Can_Compare_Two_Object_From_Classes()
    {
        var beerType1 = new BeerType1("Pilsner");
        var beerType2 = new BeerType1("Pilsner");

        Assert.False(beerType1.Equals(beerType2));
    }

    [Fact]
    public void Can_Compare_Two_Object_From_Record()
    {
        var beerType1 = new BeerType("Pilsner");
        var beerType2 = new BeerType("Pilsner");

        Assert.True(beerType1.Equals(beerType2));
    }
}