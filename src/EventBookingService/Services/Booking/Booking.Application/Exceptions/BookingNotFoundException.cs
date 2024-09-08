namespace Booking.Application.Exceptions;

public class BookingNotFoundException(Guid id) : NotFoundException("Booking", id);