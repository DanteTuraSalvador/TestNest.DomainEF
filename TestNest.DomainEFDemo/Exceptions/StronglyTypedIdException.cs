namespace TestNest.DomainEFDemo.Exceptions;

public sealed class StronglyTypedIdException : Exception
{
    public enum ErrorCode
    {
        NullInstanceCreation,
        InvalidGuidCreation,
        InvalidFormat,
        NullId
    }

    private static readonly Dictionary<ErrorCode, string> ErrorMessages = new()
    {
        { ErrorCode.NullInstanceCreation, "Failed to create instance of {0}. Activator returned null." },
        { ErrorCode.InvalidGuidCreation, "Invalid GUID provided for {0}." },
        { ErrorCode.InvalidFormat, "Invalid format." },
        { ErrorCode.NullId, "ID cannot be null or empty." }
    };

    public ErrorCode Code { get; }

    private StronglyTypedIdException(ErrorCode code, string message)
        : base(message)
    {
        Code = code;
    }

    public static StronglyTypedIdException NullInstanceCreation() =>
        new(ErrorCode.NullInstanceCreation, ErrorMessages[ErrorCode.NullInstanceCreation]);

    public static StronglyTypedIdException InvalidGuidCreation() =>
        new(ErrorCode.InvalidGuidCreation, ErrorMessages[ErrorCode.InvalidGuidCreation]);

    public static StronglyTypedIdException InvalidFormat() =>
        new(ErrorCode.InvalidFormat, ErrorMessages[ErrorCode.InvalidFormat]);

    public static StronglyTypedIdException NullId() =>
        new(ErrorCode.NullId, ErrorMessages[ErrorCode.NullId]);
}
