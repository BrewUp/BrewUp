namespace BrewUp.Production.ReadModel.Abstracts;

public abstract class DtoBase : IDtoBase
{
    public string Id { get; protected set; } = string.Empty;
    public bool IsDeleted { get; protected set; }
    public void MarkAsDeleted() => IsDeleted = true;
    public override int GetHashCode() => Id.GetHashCode();
    public override bool Equals(object? obj) => Equals((obj as DtoBase)!);
    public bool Equals(DtoBase other) => (null! != other) && (GetType() == other.GetType()) && (other.Id == Id);
    public static bool operator ==(DtoBase dto1, DtoBase dto2)
    {
        if ((object)dto1 == null && (object)dto2 == null)
            return true;
        if ((object)dto1 == null || (object)dto2 == null)
            return false;
        return ((dto1.GetType() == dto2.GetType()) && (dto1.Id == dto2.Id));
    }
    public static bool operator !=(DtoBase dto1, DtoBase dto2) => !(dto1 == dto2);
}