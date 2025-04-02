using TestNest.DomainEFDemo.Common.BaseEntity;
using TestNest.DomainEFDemo.StronglyTypeIds;
using TestNest.DomainEFDemo.ValueObjects;
using TestNest.DomainEFDemo.ValueObjects.Enums;

namespace TestNest.DomainEFDemo.Guests;

public sealed class Guest : BaseEntity<GuestId>
{
    public PersonName GuestName { get; }
    public EmailAddress GuestEmail { get; }
    public SimpleAddress GuestSimpleAddress { get; }
    public NationalityId NationalityId { get; }
    public IdTypeId IdTypeId { get; }
    public IdNumber IdNumber { get; }
    public GuestType GuestType { get; }

    private static readonly Lazy<Guest> _empty = new(() => new Guest());
    public static Guest Empty() => _empty.Value;

    private Guest() : base(GuestId.Empty())
        => (GuestName, GuestEmail, GuestSimpleAddress, NationalityId, IdTypeId, IdNumber, GuestType)
            = (PersonName.Empty(), EmailAddress.Empty(), SimpleAddress.Empty(),
               NationalityId.Empty(), IdTypeId.Empty(), IdNumber.Empty(), GuestType.None);

}