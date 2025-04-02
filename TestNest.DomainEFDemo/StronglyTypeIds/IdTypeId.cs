using TestNest.DomainEFDemo.Exceptions;
using TestNest.DomainEFDemo.StronglyTypeIds.Common;

namespace TestNest.DomainEFDemo.StronglyTypeIds;


public sealed record IdTypeId(Guid Value) : StronglyTypedId<IdTypeId>(Value)
{
    public IdTypeId() : this(Guid.NewGuid()) { }

    public static IdTypeId Create(Guid value) 
        => value == Guid.Empty ? throw StronglyTypedIdException.NullId() 
            : new IdTypeId(value);

    public override string ToString() => Value.ToString();
}