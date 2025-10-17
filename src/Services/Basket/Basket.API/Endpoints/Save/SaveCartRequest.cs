using Basket.API.Features.ShoppingBasket.Save;

namespace Basket.API.Endpoints.Save;

internal record SaveCartRequest(ShoppingCart Cart)
{
    public SaveCartCommand ToCommand() =>
        new(Cart);
}