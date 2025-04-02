namespace TestNest.DomainEFDemo.Exceptions;

public sealed class GuestTypeException : Exception
{
    public enum ErrorCode
    {
        InvalidGuestType,
        NotFound,
        UnexpectedError,
        NullGuestType
    }

    private static readonly Dictionary<ErrorCode, string> ErrorMessages = new()
        {
            { ErrorCode.InvalidGuestType, "The provided guest type is invalid." },
            { ErrorCode.NotFound, "The requested guest type was not found." },
            { ErrorCode.UnexpectedError, "An unexpected error occurred while processing guest type." },
            { ErrorCode.NullGuestType, "Guest type cannot be null or empty." }
        };

    public ErrorCode Code { get; }

    public GuestTypeException(ErrorCode code)
        : base(ErrorMessages[code])
    {
        Code = code;
    }

    public static GuestTypeException InvalidGuestType() => new(ErrorCode.InvalidGuestType);

    public static GuestTypeException NotFound() => new(ErrorCode.NotFound);

    public static GuestTypeException UnexpectedError() => new(ErrorCode.UnexpectedError);

    public static GuestTypeException NullGuestType() => new(ErrorCode.NullGuestType);
}