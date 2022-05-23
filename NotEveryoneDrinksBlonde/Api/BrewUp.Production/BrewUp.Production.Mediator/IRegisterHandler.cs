namespace BrewUp.Production.Mediator;

public interface IRegisterHandler
{
    void RegisterCommandHandlers();
    void RegisterDomainEventHandlers();
}