using BrewUpWasm.Client;
using BrewUpWasm.Modules.Production.Extensions;
using BrewUpWasm.Modules.Pubs.Extensions;
using BrewUpWasm.Shared.Configuration;
using BrewUpWasm.Shared.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using MudBlazor.Services;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

#region Configuration
builder.Services.AddSingleton(_ => builder.Configuration.GetSection("BrewUp:AppConfiguration")
    .Get<AppConfiguration>());
builder.Services.AddApplicationService();
#endregion

#region DefaultServices
builder.Services.AddScoped<LazyAssemblyLoader>();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs\\BrewUp.log")
    .CreateLogger();
#endregion

#region Modules
builder.Services.AddLogging();
builder.Services.AddProductionServices();
builder.Services.AddPubs();
#endregion

await builder.Build().RunAsync();