using Muflone.Core;

namespace BrewUp.Production.Module.Extensions.CustomTypes;

public class BeerId : DomainId
{
    public BeerId(Guid value) : base(value)
    {
    }
}