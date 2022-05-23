using BrewUpWasm.Modules.Pubs.Extensions.Abstracts;
using BrewUpWasm.Modules.Pubs.Extensions.CustomTypes;
using BrewUpWasm.Modules.Pubs.Extensions.JsonModel;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BrewUpWasm.Modules.Pubs;

public class PubsBase : ComponentBase, IDisposable
{
    [Inject] private IPubsService PubsService { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    private int _selectedRowNumber = -1;
    private BeerJson _seletectedBeer = new();

    protected IEnumerable<BeerJson> AvailableBeers = Enumerable.Empty<BeerJson>();
    protected MudTable<BeerJson> MudTable = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadAvailableBeersAsync();

        await base.OnInitializedAsync();
    }

    private async Task LoadAvailableBeersAsync()
    {
        AvailableBeers = await PubsService.GetAvailableBeersAsync(new PubId(Guid.NewGuid().ToString()));

        StateHasChanged();
    }

    protected async Task OnBrewBeerClick()
    {
        var orderBeer = new BeerJson
        {
            PubId = Guid.NewGuid().ToString(),
            PubName = "Er Grottino der Traslocatore",

            BeerId = Guid.NewGuid().ToString(),
            BeerType = "Pilsner",
            Quantity = 100
        };

        await PubsService.OrderBeerAsync(orderBeer);
    }

    protected async Task OnDrinkABeerClick()
    {
        if (string.IsNullOrEmpty(_seletectedBeer.BeerId))
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            return;
            //DialogService.Show<DialogUsageExample_Dialog>("Simple Dialog", options);
        }

        await PubsService.DrinkBeerAsync(_seletectedBeer);
    }

    protected void RowClickEvent(TableRowClickEventArgs<BeerJson> tableRowClickEventArgs)
    {
        _seletectedBeer = tableRowClickEventArgs.Item;
    }

    protected string SelectedRowClassFunc(BeerJson beer, int rowNumber)
    {
        if (_selectedRowNumber == rowNumber)
        {
            _selectedRowNumber = -1;
            return string.Empty;
        }

        if (MudTable.SelectedItem != null && MudTable.SelectedItem.Equals(beer))
        {
            _selectedRowNumber = rowNumber;
            return "selected";
        }

        return string.Empty;
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