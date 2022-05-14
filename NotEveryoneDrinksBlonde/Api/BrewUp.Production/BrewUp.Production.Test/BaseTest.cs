using System.Net.Http;
using BrewUp.Production.Module.Abstracts;
using BrewUp.Production.Module.Concretes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace BrewUp.Production.Test;

public abstract class BaseTest
{
    protected HttpClient Client;

    protected BaseTest()
    {
        var application = new GlobalAzureApplication();
        Client = application.CreateClient();
    }

    private class GlobalAzureApplication : WebApplicationFactory<Program>
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
}