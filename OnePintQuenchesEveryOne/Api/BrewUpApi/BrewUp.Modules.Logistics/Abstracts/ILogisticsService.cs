using BrewUp.Modules.Logistics.Extensions.CustomTypes;

namespace BrewUp.Modules.Logistics.Abstracts;

public interface ILogisticsService
{
    Task SendItemsAsync(Address address, City city);
}