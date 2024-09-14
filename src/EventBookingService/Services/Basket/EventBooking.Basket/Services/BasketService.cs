namespace EventBooking.Basket.Services;

[Authorize(AuthenticationSchemes = "basket_service")]
public class BasketService 
    (IBasketRepository repository)
    : BasketProtoService.BasketProtoServiceBase
{
    public override async Task<Protos.EventCart> GetEventCart(GetEventCartRequest request, ServerCallContext context)
    {
        var eventCart = await repository.GetBasketAsync(Guid.Parse(request.UserId), context.CancellationToken);

        var eventCartProto = ToEventCartProto(eventCart);
        
        return eventCartProto;  
    }

    private static Protos.EventCart ToEventCartProto(EventCart eventCart)
    {
        return new Protos.EventCart
        {
            Items =
            {
                eventCart.Items.Select(item => new Protos.EventCartItem
                {
                    EventId = item.EventId.ToString(),
                    EventLocationId = item.EventLocationId.ToString(),
                    EventName = item.EventName,
                    Id = item.Id.ToString(),
                    Price = (double)item.Price,
                    Quantity = item.Quantity,
                    StartDateTime = item.StartDateTime.ToString()
                })
            },
            TotalPrice = (double)eventCart.TotalPrice,
            UserId = eventCart.UserId.ToString()
        };
    }
}