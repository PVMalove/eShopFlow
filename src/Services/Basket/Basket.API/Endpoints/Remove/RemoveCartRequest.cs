using Basket.API.Features.ShoppingBasket.Remove;

namespace Basket.API.Endpoints.Remove;

internal record RemoveCartRequest(string AccountName)
{
    public RemoveCartCommand ToCommand() =>
        new(AccountName);
}