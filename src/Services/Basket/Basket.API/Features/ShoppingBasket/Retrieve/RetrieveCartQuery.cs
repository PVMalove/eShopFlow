namespace Basket.API.Features.ShoppingBasket.Retrieve;

public record RetrieveCartQuery(string AccountName) : IQuery<RetrieveCartResult>;