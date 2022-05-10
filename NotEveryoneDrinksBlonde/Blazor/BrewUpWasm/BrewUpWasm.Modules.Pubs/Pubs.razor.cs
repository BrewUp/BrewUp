using BrewUpWasm.Modules.Pubs.Extensions.Abstracts;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Modules.Pubs;

public class PubsBase : ComponentBase, IDisposable
{
    [Inject] private IPubsService PubsService { get; set; }

    protected string Message = "Hello from Pubs";

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