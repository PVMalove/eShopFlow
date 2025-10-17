using Basket.API.Features.ShoppingBasket.Save;

namespace Basket.API.Endpoints.Save;

public record SaveCartRequest(ShoppingCart Cart)
{
    public SaveCartCommand ToCommand() =>
        new(Cart);
};