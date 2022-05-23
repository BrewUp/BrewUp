using Muflone.Core;

namespace BrewUp.Pubs.Module.Extensions.CustomTypes;

public sealed class PubId : DomainId
{
    public PubId(Guid value) : base(value)
    {
    }
}