namespace BrewUpWasm.Modules.Production.Extensions.Abstracts;

public interface IProductionService
{
    Task<string> SayHelloAsync();
}