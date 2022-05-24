using Muflone.Core;

namespace BrewUp.Production.Module.Extensions.CustomTypes;

public sealed class PubId : DomainId
{
    public PubId(Guid value) : base(value)
    {
    }
}