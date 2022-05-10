using BrewUpWasm.Modules.Pubs.Extensions.Abstracts;
using BrewUpWasm.Modules.Pubs.Extensions.JsonModel;
using BrewUpWasm.Shared.Abstracts;
using BrewUpWasm.Shared.Concretes;
using BrewUpWasm.Shared.Configuration;
using Microsoft.Extensions.Logging;

namespace BrewUpWasm.Modules.Pubs.Extensions.Concretes;

public sealed class PubsService : BaseHttpService, IPubsService
{
    public PubsService(HttpClient httpClient, IHttpService httpService, AppConfiguration appConfiguration,
        ILoggerFactory loggerFactory) : base(httpClient, httpService, appConfiguration, loggerFactory)
    {
    }

    public async Task OrderBeerAsync(OrderBeerJson order)
    {
        try
        {
            await HttpService.Post(
                $"{AppConfiguration.ProductionApiUri}/Order/", order);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}