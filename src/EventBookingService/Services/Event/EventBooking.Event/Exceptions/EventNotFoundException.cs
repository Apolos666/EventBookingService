using BuildingBlocks.Exceptions;

namespace EventBooking.Event.Exceptions;

public class EventNotFoundException(object key) : NotFoundException("Event", key);