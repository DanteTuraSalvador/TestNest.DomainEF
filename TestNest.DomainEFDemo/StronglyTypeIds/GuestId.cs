using TestNest.DomainEFDemo.Exceptions;
using TestNest.DomainEFDemo.StronglyTypeIds.Common;

namespace TestNest.DomainEFDemo.StronglyTypeIds;

public sealed record GuestId(Guid Value) : StronglyTypedId<GuestId>(Value)
{
    public GuestId() : this(Guid.NewGuid()) { }

    public static GuestId Create(Guid value) 
        => value == Guid.Empty ? throw StronglyTypedIdException.NullId() 
            : new GuestId(value);

    public override string ToString() => Value.ToString();
}