using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Client.Shared;

public class MainLayoutBase : LayoutComponentBase, IDisposable
{
    protected bool DrawerOpen = true;

    protected void DrawerToggle()
    {
        DrawerOpen = !DrawerOpen;
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
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~MainLayoutBase()
    {
        Dispose(false);
    }
    #endregion
}