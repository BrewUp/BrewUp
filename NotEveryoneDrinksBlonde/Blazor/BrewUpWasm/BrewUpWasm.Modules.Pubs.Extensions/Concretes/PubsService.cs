using BrewUpWasm.Modules.Pubs.Extensions.Abstracts;
using BrewUpWasm.Modules.Pubs.Extensions.CustomTypes;
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

    public async Task OrderBeerAsync(BeerJson order)
    {
        try
        {
            await HttpService.Post(
                $"{AppConfiguration.PubsApiUri}pubs/beers", order);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task DrinkBeerAsync(BeerJson beerToDrink)
    {
        try
        {
            await HttpService.Put(
                $"{AppConfiguration.PubsApiUri}pubs/beers", beerToDrink);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<IEnumerable<BeerJson>> GetAvailableBeersAsync(PubId pubId)
    {
        try
        {
            var beers = new List<BeerJson>
            {
                new()
                {
                    PubId = pubId.ToString(),
                    PubName = "Er Grottino der Traslocatore",
                    BeerType = "Pilsner",
                    Quantity = 100
                },
                new()
                {
                    PubId = pubId.ToString(),
                    PubName = "Er Grottino der Traslocatore",
                    BeerType = "IPA",
                    Quantity = 50
                },
                new()
                {
                    PubId = pubId.ToString(),
                    PubName = "Er Grottino der Traslocatore",
                    BeerType = "Weiss",
                    Quantity = 100
                }
            };

            return beers;

            //return await HttpService.Get<IEnumerable<BeerJson>>(
            //    $"{AppConfiguration.PubsApiUri}/{pubId.Value}/Stock/");
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public Task<IEnumerable<BeerConsumedJson>> GetBeerConsumedAsync()
    {
        try
        {
            var beerConsumed = Enumerable.Empty<BeerConsumedJson>();
            beerConsumed = beerConsumed.Concat(new List<BeerConsumedJson>
            {
                new()
                {
                    BeerType = "IPA",
                    Quantity = 77
                },
                new()
                {
                    BeerType = "Pilsner",
                    Quantity = 25
                },
                new()
                {
                    BeerType = "Lager",
                    Quantity = 20
                },
                new()
                {
                    BeerType = "Pale ALE",
                    Quantity = 5
                }
            });

            return Task.FromResult(beerConsumed);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}