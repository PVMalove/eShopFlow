namespace Basket.API.Features.ShoppingBasket.Save;

public record SaveCartCommand(ShoppingCart Cart) : ICommand<SaveCartResult>;