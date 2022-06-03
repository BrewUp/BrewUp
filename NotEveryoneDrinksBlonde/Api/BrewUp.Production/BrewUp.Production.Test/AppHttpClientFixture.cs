using System;
using System.Net.Http;
using BrewUp.Production.Module.Abstracts;
using BrewUp.Production.Module.Concretes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BrewUp.Production.Test;

public class AppHttpClientFixture : IDisposable
{
    public readonly HttpClient Client;

    public AppHttpClientFixture()
    {
        var app = new LicenseApplication();
        Client = app.CreateClient();
    }

    private class LicenseApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddLogging();

                services.AddScoped<IProductionService, ProductionService>();
            });

            return base.CreateHost(builder);
        }
    }

    #region Dispose
    public void Dispose(bool disposing)
    {
        if (disposing)
        {
            Client.Dispose();
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~AppHttpClientFixture()
    {
        Dispose(false);
    }
    #endregion
}