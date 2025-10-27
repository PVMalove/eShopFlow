namespace Basket.API.Features.ShoppingBasket.Remove;

public record RemoveCartCommand(string AccountName) : ICommand<RemoveCartResult>;