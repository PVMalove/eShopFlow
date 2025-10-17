using Basket.API.Features.ShoppingBasket.Retrieve;

namespace Basket.API.Endpoints.Retrieve;

internal record RetrieveCartRequest(string AccountName)
{
    public RetrieveCartQuery ToQuery() =>
        new(AccountName);
}