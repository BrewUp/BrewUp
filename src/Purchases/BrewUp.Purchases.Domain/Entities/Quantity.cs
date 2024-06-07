using Muflone.Core;

namespace Brewup.Purchases.Domain.Entities;

public class Quantity : ValueObject
{
	public double Value { get; }
	public string UnitOfMeasure { get; }

	public Quantity(double value, string unitOfMeasure)
	{
		Value = value;
		UnitOfMeasure = unitOfMeasure;
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
		yield return UnitOfMeasure;
	}
}