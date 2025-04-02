using TestNest.DomainEFDemo.ValueObjects.Common;

namespace TestNest.DomainEFDemo.ValueObjects;

public sealed class Address : ValueObject
{
    public string AddressLine { get; }
    public string City { get; }
    
    private static readonly Lazy<Address> _empty = new(() => new Address());
    public static Address Empty() => _empty.Value;

    public Address()  => (AddressLine, City) = (string.Empty, string.Empty);

    protected override IEnumerable<object> GetAtomicValues() 
        => new object[] { AddressLine, City };

    public override string ToString() 
        => $"{AddressLine}, {City}";
    
    // other method ommitted for brevity,
    // like the factory method Create and Update methods   
}