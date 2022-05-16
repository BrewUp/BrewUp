using BrewUpWasm.Modules.Login.Extensions.JsonModel;
using Microsoft.AspNetCore.Components;

namespace BrewUpWasm.Modules.Login;

public class LoginBase : ComponentBase, IDisposable
{
    protected LoginModel LoginModel = new ();

    protected async Task HandleValidSubmit()
    {

    }

    protected void HandleInvalidSubmit()
    {}

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

    ~LoginBase()
    {
        this.Dispose(false);
    }
    #endregion
}