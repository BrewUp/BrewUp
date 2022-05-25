using Microsoft.AspNetCore.Components;
using MudBlazor.ThemeManager;

namespace BrewUpWasm.Client.Shared;

public class MainLayoutBase : LayoutComponentBase, IDisposable
{
    protected bool DrawerOpen = true;
    protected ThemeManagerTheme ThemeManager = new ();
    public bool ThemeManagerOpen;

    protected override void OnInitialized()
    {
        StateHasChanged();
    }

    protected void DrawerToggle()
    {
        DrawerOpen = !DrawerOpen;
    }

    protected void OpenThemeManager(bool value)
    {
        ThemeManagerOpen = value;
    }

    protected void UpdateTheme(ThemeManagerTheme value)
    {
        ThemeManager = value;
        StateHasChanged();
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