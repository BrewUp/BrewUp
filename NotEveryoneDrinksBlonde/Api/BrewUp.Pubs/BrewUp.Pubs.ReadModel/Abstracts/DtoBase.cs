namespace BrewUp.Pubs.ReadModel.Abstracts;

public abstract class DtoBase : IDtoBase
{
    public string Id { get; protected set; } = string.Empty;
    public bool IsDeleted { get; protected set; }
    public void MarkAsDeleted() => IsDeleted = true;
    public override int GetHashCode() => Id.GetHashCode();
    public override bool Equals(object obj) => Equals(obj as DtoBase);
    public bool Equals(DtoBase other) => (null != other) && (GetType() == other.GetType()) && (other.Id == Id);
    public static bool operator ==(DtoBase Dto1, DtoBase Dto2)
    {
        if ((object)Dto1 == null && (object)Dto2 == null)
            return true;
        if ((object)Dto1 == null || (object)Dto2 == null)
            return false;
        return ((Dto1.GetType() == Dto2.GetType()) && (Dto1.Id == Dto2.Id));
    }
    public static bool operator !=(DtoBase Dto1, DtoBase Dto2) => !(Dto1 == Dto2);
}