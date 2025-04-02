using TestNest.DomainEFDemo.ValueObjects.Common;

namespace TestNest.DomainEFDemo.ValueObjects;

public sealed class EmailAddress : ValueObject
{
    public string Value { get; }

    private static readonly Lazy<EmailAddress> _empty = new(() => new EmailAddress());
    public static EmailAddress Empty() => _empty.Value;

    private EmailAddress() => Value = string.Empty;
    
    protected override IEnumerable<object?> GetAtomicValues() { yield return Value; }

    public override string ToString() => Value;

    // other method ommitted for brevity,
    // like the factory method Create and Update methods
}
