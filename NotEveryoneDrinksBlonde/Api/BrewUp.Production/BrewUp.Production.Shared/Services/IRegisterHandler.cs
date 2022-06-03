namespace BrewUp.Production.Shared.Services;

public interface IRegisterHandler
{
    void RegisterCommandHandlers();
    void RegisterDomainEventHandlers();
}