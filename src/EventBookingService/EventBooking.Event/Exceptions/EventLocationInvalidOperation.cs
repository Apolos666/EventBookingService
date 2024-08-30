namespace EventBooking.Event.Exceptions;

public class EventLocationInvalidOperation(string operation, string reason)
    : InvalidOperationException("EventLocation", operation, reason);