namespace BrewUpWasm.Modules.Production.Extensions.CustomTypes;

public record BeerType
{
    public string Value { get; init; }

    public BeerType(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new Exception("");

        Value = value;
    }
}

public class BeerType1
{
    public readonly string Value;

    public BeerType1(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new Exception("");

        Value = value;
    }
}
