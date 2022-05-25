using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

namespace BrewUpWasm.Client;

public class AppBase : ComponentBase, IDisposable
{
    [Inject] private LazyAssemblyLoader AssemblyLoader { get; set; } = default!;
    [Inject] private ILogger<App> Logger { get; set; } = default!;

    protected readonly List<Assembly> LazyLoadedAssemblies = new();

    protected async Task OnNavigateAsync(NavigationContext args)
    {
        try
        {
            switch (args.Path)
            {
                case "production":
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "BrewUpWasm.Modules.Production.dll"
                        });
                        LazyLoadedAssemblies.AddRange(assemblies);
                        break;
                    }

                case "pubs":
                {
                    var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                    {
                        "BrewUpWasm.Modules.Pubs.dll"
                    });
                    LazyLoadedAssemblies.AddRange(assemblies);
                    break;
                }

                default:
                {
                    var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                    {
                        "BrewUpWasm.Modules.Dashboard.dll"
                    });
                    LazyLoadedAssemblies.AddRange(assemblies);
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error Loading spares page: {ex}");
        }
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

    ~AppBase()
    {
        Dispose(false);
    }
    #endregion
}