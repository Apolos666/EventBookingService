using BuildingBlocks.Exceptions;

namespace EventBooking.Basket.Exceptions;

public class BasketNotFoundException(object key) : NotFoundException("Basket", key);

