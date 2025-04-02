using TestNest.DomainEFDemo.Exceptions;
using TestNest.DomainEFDemo.StronglyTypeIds.Common;

namespace TestNest.DomainEFDemo.StronglyTypeIds;

public sealed record NationalityId(Guid Value) : StronglyTypedId<NationalityId>(Value)
{
    public NationalityId() : this(Guid.NewGuid()) { }

    public static NationalityId Create(Guid value) 
        => value == Guid.Empty ? throw StronglyTypedIdException.NullId() 
            : new NationalityId(value);

    public override string ToString() => Value.ToString();
}