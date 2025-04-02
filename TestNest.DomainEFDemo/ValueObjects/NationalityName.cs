using System.Text.RegularExpressions;
using TestNest.DomainEFDemo.ValueObjects.Common;

namespace TestNest.DomainEFDemo.ValueObjects;

public sealed class NationalityName : ValueObject
{
    private static readonly Lazy<NationalityName> _empty = new(() => new NationalityName());
    public static NationalityName Empty() => _empty.Value;

    public string Value { get; }
    private NationalityName() => Value = string.Empty;
    
    protected override IEnumerable<object?> GetAtomicValues() { yield return Value; }

    public override string ToString() => Value;

    // other method ommitted for brevity,
    // like the factory method Create and Update methods
}