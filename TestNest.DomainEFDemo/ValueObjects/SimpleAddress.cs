using TestNest.DomainEFDemo.ValueObjects.Common;

namespace TestNest.DomainEFDemo.ValueObjects;

public sealed class SimpleAddress : ValueObject
{
    public Address Address { get; }
    public string Country { get; }

    private static readonly Lazy<SimpleAddress> _empty = new(() => new SimpleAddress());
    public static SimpleAddress Empty() => _empty.Value;

    public SimpleAddress() => (Address, Country) = (Address.Empty(), string.Empty);

    protected override IEnumerable<object> GetAtomicValues() 
        => new object[] { Address, Country };

    public override string ToString() => $"{Address.AddressLine}, {Address.City}, {Country}";
}