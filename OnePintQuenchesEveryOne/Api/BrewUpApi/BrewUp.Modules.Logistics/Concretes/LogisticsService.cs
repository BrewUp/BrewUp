using BrewUp.Modules.Logistics.Abstracts;
using BrewUp.Modules.Logistics.Extensions.CustomTypes;
using BrewUp.Modules.Logistics.ReadModel.Abstracts;
using BrewUp.Modules.Logistics.ReadModel.Dtos;

namespace BrewUp.Modules.Logistics.Concretes;

public class LogisticsService : ILogisticsService
{
    private readonly IPersister _persister;

    public LogisticsService(IPersister persister)
    {
        _persister = persister;
    }

    public async Task SendItemsAsync(Address address, City city)
    {
        var customer = await _persister.GetByIdAsync<Customer>("123");
    }
}