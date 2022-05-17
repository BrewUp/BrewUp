using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Production.Client.Features;

public class IndexBase : ComponentBase, IDisposable
{
    [Inject] private ISessionStorageService _sessionStorageService { get; set; }

    protected string HelloMessage = "Nothing to Tell :-(";

    protected override async Task OnInitializedAsync()
    {
        HelloMessage = await _sessionStorageService.GetItemAsync<string>("HelloMessage");

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