namespace EventBooking.Basket.Services;

[Authorize(AuthenticationSchemes = "basket_service")]
public class BasketService 
    (IBasketRepository repository)
    : BasketProtoService.BasketProtoServiceBase
{
    public override async Task<Protos.EventCart> GetEventCart(GetEventCartRequest request, ServerCallContext context)
    {
        var eventCart = await repository.GetBasketAsync(Guid.Parse(request.UserId), context.CancellationToken);

        var eventCartProto = eventCart.Adapt<Protos.EventCart>();
        
        return eventCartProto;  
    }
}