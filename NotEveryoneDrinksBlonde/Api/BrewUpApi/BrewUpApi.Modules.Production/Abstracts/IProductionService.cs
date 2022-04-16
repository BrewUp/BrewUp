namespace BrewUpApi.Modules.Production.Abstracts;

public interface IProductionService
{
    Task<string> SayHelloAsync();
}