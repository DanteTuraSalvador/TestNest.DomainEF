using System.Collections.Immutable;
using TestNest.DomainEFDemo.Exceptions;

namespace TestNest.DomainEFDemo.ValueObjects.Enums;

public sealed class GuestType
{
    public enum GuestTypeId { None = -1, Primary = 0, Companion = 1, FamilyMember = 2, EventAttendees = 3 }

    private static readonly ImmutableDictionary<GuestTypeId, GuestType> _statuses =
        new Dictionary<GuestTypeId, GuestType>
        {
            { GuestTypeId.None, new GuestType(GuestTypeId.None, "None") },
            { GuestTypeId.Primary, new GuestType(GuestTypeId.Primary, "Primary") },
            { GuestTypeId.Companion, new GuestType(GuestTypeId.Companion, "Companion") },
            { GuestTypeId.FamilyMember, new GuestType(GuestTypeId.FamilyMember, "FamilyMember") },
            { GuestTypeId.EventAttendees, new GuestType(GuestTypeId.EventAttendees, "EventAttendees") }
        }.ToImmutableDictionary();

    private static readonly ImmutableDictionary<string, GuestType> _statusesByName =
        _statuses.Values.ToImmutableDictionary(s => s.Name.ToLowerInvariant(), s => s);

    public GuestTypeId Id { get; }
    public string Name { get; }

    private GuestType(GuestTypeId id, string name) => (Id, Name) = (id, name);

    public override string ToString() => Name;

    public static readonly GuestType None = _statuses[GuestTypeId.None];
    public static readonly GuestType Primary = _statuses[GuestTypeId.Primary];
    public static readonly GuestType Companion = _statuses[GuestTypeId.Companion];
    public static readonly GuestType FamilyMember = _statuses[GuestTypeId.FamilyMember];
    public static readonly GuestType EventAttendees = _statuses[GuestTypeId.EventAttendees];


    public static GuestType FromId(GuestTypeId id) 
        => _statuses.TryGetValue(id, out var status) ? status 
            : throw GuestTypeException.InvalidGuestType();

    public static GuestType FromName(string name) 
        => _statusesByName.TryGetValue(name.ToLowerInvariant(), out var status) ? status 
            : throw GuestTypeException.InvalidGuestType();

    public static GuestType Of(GuestTypeId id) 
        => FromId(id);

    public static implicit operator GuestType(GuestTypeId id) 
        => FromId(id);

    public static IReadOnlyCollection<GuestType> All 
        => _statuses.Values.ToImmutableList();

    public static GuestType Empty => None;
}
