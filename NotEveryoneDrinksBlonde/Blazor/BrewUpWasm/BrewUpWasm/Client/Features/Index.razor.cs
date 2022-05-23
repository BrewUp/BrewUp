using Blazored.SessionStorage;
using BrewUpWasm.Shared.Abstracts;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Client.Features;

public class IndexBase : ComponentBase, IDisposable
{
    [Inject] private ISessionStorageService SessionStorageService { get; set; } = default!;
    [Inject] private ILocalStorageService LocalStorageService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await SessionStorageService.SetItemAsync("HelloMessageSession", "Hello from SessionStorage");

        await LocalStorageService.SetItemAsync("HelloMessageLocal", "Hello from LocalStorage");

        await base.OnInitializedAsync();
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

    ~IndexBase()
    {
        this.Dispose(false);
    }
    #endregion
}