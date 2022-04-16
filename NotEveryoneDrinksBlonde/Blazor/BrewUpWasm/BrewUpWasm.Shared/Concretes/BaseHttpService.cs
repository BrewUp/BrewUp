using BrewUpWasm.Shared.Abstracts;
using BrewUpWasm.Shared.Configuration;
using Microsoft.Extensions.Logging;

namespace BrewUpWasm.Shared.Concretes
{
    public abstract class BaseHttpService
    {
        protected readonly HttpClient HttpClient;
        protected readonly IHttpService HttpService;
        protected readonly AppConfiguration AppConfiguration;
        protected readonly ILogger Logger;

        protected BaseHttpService(HttpClient httpClient,
            IHttpService httpService,
            AppConfiguration appConfiguration,
            ILoggerFactory loggerFactory)
        {
            HttpClient = httpClient;
            HttpService = httpService;
            AppConfiguration = appConfiguration;
            Logger = loggerFactory.CreateLogger(GetType());
        }
    }
}