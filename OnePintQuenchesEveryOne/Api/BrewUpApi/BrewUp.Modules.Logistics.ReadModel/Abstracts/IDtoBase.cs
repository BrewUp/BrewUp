namespace BrewUp.Modules.Logistics.ReadModel.Abstracts;

public interface IDtoBase
{
    Guid Id { get; }
    bool IsDeleted { get; }
}