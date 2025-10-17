namespace Basket.API.Features.ShoppingBasket.Save;

public record SaveCartCommand(ShoppingCart Card) : ICommand<SaveCartResult>;