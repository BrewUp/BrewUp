using BrewUpWasm.Shared.Abstracts;
using BrewUpWasm.Shared.Configuration;
using Microsoft.Extensions.Logging;

namespace BrewUpWasm.Shared.Concretes;

public class PubsService : BaseService, IPubsService
{
    public PubsService(ILoggerFactory loggerFactory,
        AppConfiguration appConfiguration) : base(loggerFactory, appConfiguration)
    {
    }
}