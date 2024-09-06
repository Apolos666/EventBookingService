namespace EventBooking.Event.Exceptions;

public class UnauthorizedEventDeletionException(string userId) : UnauthorizedActionException("delete event", userId);