using TestNest.DomainEFDemo.Common.BaseEntity;
using TestNest.DomainEFDemo.StronglyTypeIds;
using TestNest.DomainEFDemo.ValueObjects;

namespace TestNest.DomainEFDemo.Guests;

public sealed class Identification : BaseEntity<IdTypeId>
{
    public IdTypeName IdTypeName { get; }

    private static readonly Lazy<Identification> _empty = new(() 
        => new Identification());

    public static Identification Empty() 
        => _empty.Value;

    private Identification() : base(IdTypeId.Empty()) 
        => IdTypeName = IdTypeName.Empty();

    public override string ToString() 
        => IdTypeName.ToString();

    // other method ommitted for brevity,
    // like the factory method Create and Update methods
}