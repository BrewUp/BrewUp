using BrewUpWasm.Production.Shared.Configuration;
using Microsoft.Extensions.Logging;

namespace BrewUpWasm.Production.Shared.Concretes
{
    public abstract class BaseService
    {
        protected ILogger Logger;
        protected AppConfiguration AppConfiguration;

        protected BaseService(ILoggerFactory loggerFactory,
            AppConfiguration appConfiguration)
        {
            AppConfiguration = appConfiguration;
            Logger = loggerFactory.CreateLogger(GetType());
        }
    }
}