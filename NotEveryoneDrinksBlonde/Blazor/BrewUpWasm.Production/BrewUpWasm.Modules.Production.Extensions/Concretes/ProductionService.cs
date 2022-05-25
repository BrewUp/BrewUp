using BrewUpWasm.Modules.Production.Extensions.Abstracts;
using BrewUpWasm.Modules.Production.Extensions.JsonModel;
using BrewUpWasm.Production.Shared.Abstracts;
using BrewUpWasm.Production.Shared.Concretes;
using BrewUpWasm.Production.Shared.Configuration;
using Microsoft.Extensions.Logging;

namespace BrewUpWasm.Modules.Production.Extensions.Concretes;

public sealed class ProductionService : BaseHttpService, IProductionService
{
    public ProductionService(HttpClient httpClient, IHttpService httpService, AppConfiguration appConfiguration,
        ILoggerFactory loggerFactory) : base(httpClient, httpService, appConfiguration, loggerFactory)
    {
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