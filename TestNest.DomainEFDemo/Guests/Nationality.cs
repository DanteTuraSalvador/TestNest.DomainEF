using TestNest.DomainEFDemo.Common.BaseEntity;
using TestNest.DomainEFDemo.StronglyTypeIds;
using TestNest.DomainEFDemo.ValueObjects;

namespace TestNest.DomainEFDemo.Guests;

public sealed class Nationality : BaseEntity<NationalityId>
{
    public string NationalityName { get; }

    private static readonly Lazy<Nationality> _empty = new(()
        => new Nationality());

    public static Nationality Empty()
        => _empty.Value;

    private Nationality() : base(NationalityId.Empty())
        => NationalityName = string.Empty;

    public override string ToString()
        => NationalityName.ToString();

    // If we want to use the value object NationalityName
    // un-comment the following code below and comment the above code
    // make sure we have configure the Nationality configuration in Infrastructure
    // see NationalityConfiguration.cs

    //public NationalityName NationalityName { get; }

    //private static readonly Lazy<Nationality> _empty = new(() 
    //    => new Nationality());

    //public static Nationality Empty() 
    //    => _empty.Value;

    //private Nationality() : base(NationalityId.Empty()) 
    //    => NationalityName = NationalityName.Empty();

    //public override string ToString() 
    //    => NationalityName.ToString();


    // other method ommitted for brevity,
    // like the factory method Create and Update methods
}