using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Client.Features;

public class IndexBase : ComponentBase, IDisposable
{
    [Inject] private ISessionStorageService _sessionStorageService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await _sessionStorageService.SetItemAsync("HelloMessage", "Hello By General Module");

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