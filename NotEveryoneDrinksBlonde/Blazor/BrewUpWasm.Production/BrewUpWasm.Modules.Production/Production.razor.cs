using BrewUpWasm.Modules.Production.Extensions.Abstracts;
using BrewUpWasm.Modules.Production.Extensions.JsonModel;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BrewUpWasm.Modules.Production;

public class ProductionBase : ComponentBase, IDisposable
{
    [Inject] private IProductionService ProductionService { get; set; } = default!;

    protected IEnumerable<BeerJson> BeerBrewed = Enumerable.Empty<BeerJson>();
    protected MudTable<BeerJson> MudTable = default!;

    protected override async Task OnInitializedAsync()
    {
        await OnLoadBeers();

        await base.OnInitializedAsync();
    }

    protected async Task OnLoadBeers()
    {
        BeerBrewed = await ProductionService.GetBeersAsync();
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

    ~ProductionBase()
    {
        this.Dispose(false);
    }
    #endregion
}