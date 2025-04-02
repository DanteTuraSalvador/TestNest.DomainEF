using TestNest.DomainEFDemo.ValueObjects.Common;

namespace TestNest.DomainEFDemo.ValueObjects;

public sealed class IdTypeName : ValueObject
{
    public string Value { get; }

    private static readonly Lazy<IdTypeName> _empty = new(() => new IdTypeName());
    public static IdTypeName Empty() => _empty.Value;
    
    public IdTypeName() => Value = string.Empty;

    protected override IEnumerable<object?> GetAtomicValues() { yield return Value; }

    public override string ToString() => Value;

    // other method ommitted for brevity,
    // like the factory method Create and Update methods
}