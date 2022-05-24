using BrewUpWasm.Modules.Production.Extensions.Abstracts;
using BrewUpWasm.Modules.Production.Extensions.JsonModel;
using BrewUpWasm.Shared.Abstracts;
using BrewUpWasm.Shared.Concretes;
using BrewUpWasm.Shared.Configuration;
using Microsoft.Extensions.Logging;

namespace BrewUpWasm.Modules.Production.Extensions.Concretes;

public sealed class ProductionService : BaseHttpService, IProductionService
{
    public ProductionService(HttpClient httpClient, IHttpService httpService, AppConfiguration appConfiguration,
        ILoggerFactory loggerFactory) : base(httpClient, httpService, appConfiguration, loggerFactory)
    {
    }

    public Task<string> SayHelloAsync()
    {
        var greetings = "Hello from Production Module";

        return Task.FromResult(greetings);
    }

    public async Task<IEnumerable<BeerJson>> GetBeersAsync()
    {
        try
        {
            return await HttpService.Get<IEnumerable<BeerJson>>(
                $"{AppConfiguration.ProductionApiUri}production/Beers");
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}