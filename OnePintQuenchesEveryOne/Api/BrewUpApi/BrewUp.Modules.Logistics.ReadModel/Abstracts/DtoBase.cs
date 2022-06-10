namespace BrewUp.Modules.Logistics.ReadModel.Abstracts;

public abstract class DtoBase : IDtoBase
{
    public Guid Id { get; protected set; }
    public bool IsDeleted { get; protected set; }
}