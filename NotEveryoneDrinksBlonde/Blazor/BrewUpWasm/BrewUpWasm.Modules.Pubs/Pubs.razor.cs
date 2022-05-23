using BrewUpWasm.Modules.Pubs.Extensions.Abstracts;
using BrewUpWasm.Modules.Pubs.Extensions.JsonModel;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Modules.Pubs;

public class PubsBase : ComponentBase, IDisposable
{
    [Inject] private IPubsService PubsService { get; set; } = default!;

    protected async Task OnBrewBeerClick()
    {
        var orderBeer = new OrderBeerJson
        {
            PubId = Guid.NewGuid().ToString(),
            PubName = "Er Grottino der Traslocatore",

            BeerId = Guid.NewGuid().ToString(),
            BeerType = "Pilsner",
            Quantity = 100
        };

        await PubsService.OrderBeerAsync(orderBeer);
    }

    #region Dispose
    public void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~PubsBase()
    {
        this.Dispose(false);
    }
    #endregion
}