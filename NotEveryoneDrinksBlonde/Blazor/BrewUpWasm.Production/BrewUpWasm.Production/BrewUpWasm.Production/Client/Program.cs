using Blazored.SessionStorage;
using BrewUpWasm.Modules.Production.Extensions;
using BrewUpWasm.Production.Client;
using BrewUpWasm.Production.Shared.Configuration;
using BrewUpWasm.Production.Shared.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using MudBlazor.Services;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#region Configuration
builder.Services.AddSingleton(_ => builder.Configuration.GetSection("BrewUp:AppConfiguration")
    .Get<AppConfiguration>());
builder.Services.AddApplicationService();
#endregion

#region DefaultServices
builder.Services.AddScoped<LazyAssemblyLoader>();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

builder.Services.AddMudServices();
builder.Services.AddBlazoredSessionStorage();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs\\BrewUp-Production.log")
    .CreateLogger();
#endregion

#region Modules
builder.Services.AddProductionServices();
#endregion



await builder.Build().RunAsync();