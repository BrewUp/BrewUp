using BrewUpWasm.Modules.Pubs.Extensions.Abstracts;
using BrewUpWasm.Modules.Pubs.Extensions.JsonModel;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Modules.Dashboard;

public class StockBeerBase : ComponentBase, IDisposable
{
    [Inject] private IPubsService PubsService { get; set; }

    protected IEnumerable<BeerConsumedJson> BeerConsumed { get; set; } = Enumerable.Empty<BeerConsumedJson>();

    protected double[] Data = Enumerable.Empty<double>().ToArray();
    protected string[] Labels = Enumerable.Empty<string>().ToArray();

    protected override async Task OnInitializedAsync()
    {
        await GetConsumedBeerAsync();
        await base.OnInitializedAsync();
    }

    private async Task GetConsumedBeerAsync()
    {
        BeerConsumed = await PubsService.GetBeerConsumedAsync();
        var beerConsumedArray = BeerConsumed as BeerConsumedJson[] ?? BeerConsumed.ToArray();

        Data = beerConsumedArray.Select(b => b.Quantity).ToArray();
        Labels = beerConsumedArray.Select(b => b.BeerType).ToArray();

        StateHasChanged();
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

    ~StockBeerBase()
    {
        this.Dispose(false);
    }
    #endregion
}