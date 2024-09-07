namespace Booking.Domain.ValueObjects;

public record EventName
{
    private const int DefaultMaxLength = 100;
    public string Value { get; }
    private EventName(string value) => Value = value;

    public static EventName Of(string value)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length > DefaultMaxLength)
            throw new DomainException($"Event name cannot be longer than {DefaultMaxLength} characters.");

        return new EventName(value);
    }
}