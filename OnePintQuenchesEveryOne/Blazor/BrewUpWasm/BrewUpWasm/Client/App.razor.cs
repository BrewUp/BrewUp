using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Client;

public class AppBase : ComponentBase, IDisposable
{
    #region Dispose
    public void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~AppBase()
    {
        Dispose(false);
    }
    #endregion
}