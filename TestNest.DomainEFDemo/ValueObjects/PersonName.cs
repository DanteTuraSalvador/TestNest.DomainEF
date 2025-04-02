using TestNest.DomainEFDemo.ValueObjects.Common;

namespace TestNest.DomainEFDemo.ValueObjects;
public sealed class PersonName : ValueObject
{
    private static readonly Lazy<PersonName> _empty = new(() => new PersonName());
    public static PersonName Empty() => _empty.Value;

    public string FirstName { get; }
    public string? MiddleName { get; }
    public string LastName { get; }

    private PersonName() 
        => (this.FirstName, this.MiddleName, this.LastName) 
            = (String.Empty, String.Empty, String.Empty);
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return this.FirstName;
        if (this.MiddleName is not null) yield return MiddleName;
        yield return this.LastName;
    }

    public override string ToString() => GetFullName();

    public string GetFullName()
    {
        return string.IsNullOrWhiteSpace(this.MiddleName)
            ? $"{this.FirstName} {this.LastName}"
            : $"{this.FirstName} {this.MiddleName} {this.LastName}";
    }
}