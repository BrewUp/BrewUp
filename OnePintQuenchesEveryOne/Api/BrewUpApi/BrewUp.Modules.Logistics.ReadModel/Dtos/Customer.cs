using BrewUp.Modules.Logistics.Extensions.CustomTypes;
using BrewUp.Modules.Logistics.ReadModel.Abstracts;

namespace BrewUp.Modules.Logistics.ReadModel.Dtos;

public class Customer : DtoBase
{
    public string RagioneSociale { get; private set; } = string.Empty;

    protected Customer()
    {}

    public static Customer CreateCustomer(CustomerId customerId, RagioneSociale ragione) =>
        new(customerId.Value.ToString());

    private Customer(string customerId)
    {

    }
}