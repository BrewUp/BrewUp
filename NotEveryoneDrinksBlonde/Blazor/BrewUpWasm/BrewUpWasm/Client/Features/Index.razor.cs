using Blazored.SessionStorage;
using BrewUpWasm.Shared.Abstracts;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Client.Features;

public class IndexBase : ComponentBase, IDisposable
{
    [Inject] private ISessionStorageService _sessionStorageService { get; set; }
    [Inject] private ILocalStorageService _localStorageService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await _sessionStorageService.SetItemAsync("HelloMessageSession", "Hello from SessionStorage");

        await _localStorageService.SetItemAsync("HelloMessageLocal", "Hello from LocalStorage");

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