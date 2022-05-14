using Muflone.Core;

namespace BrewUp.Pubs.Module.Extensions.CustomTypes;

public class BeerId : DomainId
{
    public BeerId(Guid value) : base(value)
    {
    }
}