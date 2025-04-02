using TestNest.DomainEFDemo.ValueObjects.Common;

namespace TestNest.DomainEFDemo.ValueObjects;

public sealed class IdNumber : ValueObject
{
    private static readonly Lazy<IdNumber> _empty = new(() => new IdNumber());
    public static IdNumber Empty() => _empty.Value;

    public string Value { get; }
    public IdNumber() => Value = string.Empty;
    
    protected override IEnumerable<object?> GetAtomicValues() { yield return Value; }

    public override string ToString() => Value;

    // other method ommitted for brevity,
    // like the factory method Create and Update methods
}