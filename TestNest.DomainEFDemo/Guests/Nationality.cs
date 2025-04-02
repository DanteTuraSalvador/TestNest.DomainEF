using TestNest.DomainEFDemo.Common.BaseEntity;
using TestNest.DomainEFDemo.StronglyTypeIds;
using TestNest.DomainEFDemo.ValueObjects;

namespace TestNest.DomainEFDemo.Guests;

public sealed class Nationality : BaseEntity<NationalityId>
{
    public NationalityName NationalityName { get; }

    private static readonly Lazy<Nationality> _empty = new(() 
        => new Nationality());

    public static Nationality Empty() 
        => _empty.Value;

    private Nationality() : base(NationalityId.Empty()) 
        => NationalityName = NationalityName.Empty();
     
    public override string ToString() 
        => NationalityName.ToString();
}