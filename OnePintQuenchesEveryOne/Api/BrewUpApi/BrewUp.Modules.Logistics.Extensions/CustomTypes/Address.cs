namespace BrewUp.Modules.Logistics.Extensions.CustomTypes;

public record Address
{
    public Address(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }

    public string Value { get; init; }
}

public class Address1
{
    public readonly string Value;

    public Address1(string value)
    {
        Value = value;
    }
}