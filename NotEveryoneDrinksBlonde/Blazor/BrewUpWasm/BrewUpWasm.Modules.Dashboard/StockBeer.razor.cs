using BrewUpWasm.Modules.Pubs.Extensions.Abstracts;
using BrewUpWasm.Modules.Pubs.Extensions.JsonModel;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Modules.Dashboard;

public class StockBeerBase : ComponentBase, IDisposable
{
    [Inject] private IPubsService PubsService { get; set; }

    protected IEnumerable<BeerConsumed> BeerConsumed { get; set; } = Enumerable.Empty<BeerConsumed>();

    protected double[] Data;
    protected string[] Labels;

    protected override async Task OnInitializedAsync()
    {
        await GetConsumedBeerAsync();
        await base.OnInitializedAsync();
    }

    private async Task GetConsumedBeerAsync()
    {
        BeerConsumed = await PubsService.GetBeerConsumedAsync();
        var beerConsumedArray = BeerConsumed as BeerConsumed[] ?? BeerConsumed.ToArray();

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