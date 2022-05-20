using Microsoft.Extensions.Logging;

namespace BrewUpWasm.Shared.Abstracts;

public abstract class BaseService
{
    protected IHttpService HttpService;
    protected ILogger Logger;

    protected BaseService(IHttpService httpService,
        ILoggerFactory loggerFactory)
    {
        HttpService = httpService;
        Logger = loggerFactory.CreateLogger(GetType());
    }
}