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

    private int _orderIndex = 1;

    protected IEnumerable<BeerJson> AvailableBeers = Enumerable.Empty<BeerJson>();
    protected MudTable<BeerJson> MudTable = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadAvailableBeersAsync();

        await base.OnInitializedAsync();
    }

    private async Task LoadAvailableBeersAsync()
    {
        AvailableBeers = await PubsService.GetAvailableBeersAsync(new PubId("4a89a5cc-40be-4dcf-b10e-bd4014243105"));

        StateHasChanged();
    }

    protected async Task LoadBeersForGrottinoAsync()
    {
        AvailableBeers = await PubsService.GetAvailableBeersAsync(new PubId("4a89a5cc-40be-4dcf-b10e-bd4014243105"));
    }

    protected async Task LoadBeersForFraschettaAsync()
    {
        AvailableBeers = await PubsService.GetAvailableBeersAsync(new PubId("d78e9e31-5396-4991-817a-84b6836db918"));
    }

    protected async Task OnBrewBeerClick()
    {
        BeerJson orderBeer;
        if (_orderIndex.Equals(1))
        {
            orderBeer = new BeerJson
            {
                PubId = "4a89a5cc-40be-4dcf-b10e-bd4014243105",
                PubName = "Er Grottino der Traslocatore",

                BeerId = "17480605-6183-4820-9267-5ae36ef6c8a8",
                BeerType = "Pilsner",
                Quantity = 100
            };

            _orderIndex = 0;
        }
        else
        {
            orderBeer = new BeerJson
            {
                PubId = "d78e9e31-5396-4991-817a-84b6836db918",
                PubName = "La Fraschetta di Grotta Perfetta",

                BeerId = "9502a2d4-453c-4f9e-8bea-baa4a727cec5",
                BeerType = "Weiss",
                Quantity = 200
            };

            _orderIndex = 1;
        }

        await PubsService.OrderBeerAsync(orderBeer);

        Thread.Sleep(1000);

        await LoadAvailableBeersAsync();
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