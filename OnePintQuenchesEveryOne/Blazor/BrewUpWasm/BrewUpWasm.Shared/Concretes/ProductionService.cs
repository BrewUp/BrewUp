using BrewUpWasm.Shared.Abstracts;
using BrewUpWasm.Shared.Configuration;
using Microsoft.Extensions.Logging;

namespace BrewUpWasm.Shared.Concretes;

public class ProductionService : BaseService, IProductionService
{
    public ProductionService(ILoggerFactory loggerFactory,
        AppConfiguration appConfiguration)
        : base(loggerFactory, appConfiguration)
    {
    }
}