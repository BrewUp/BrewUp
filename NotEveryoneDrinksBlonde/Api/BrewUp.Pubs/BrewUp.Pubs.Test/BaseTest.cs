using System.Net.Http;
using BrewUp.Pubs.Module.Abstracts;
using BrewUp.Pubs.Module.Concretes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace BrewUp.Pubs.Test;

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

                services.AddScoped<IPubsService, PubsService>();
            });

            return base.CreateHost(builder);
        }
    }
}