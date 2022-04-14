using BrewUpWasm.Modules.Production.Extensions.Abstracts;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Modules.Production;

public class ProductionBase : ComponentBase, IDisposable
{
    [Inject] private IProductionService ProductionService { get; set; }

    protected string Message = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Message = await ProductionService.SayHelloAsync();

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

    ~ProductionBase()
    {
        this.Dispose(false);
    }
    #endregion
}