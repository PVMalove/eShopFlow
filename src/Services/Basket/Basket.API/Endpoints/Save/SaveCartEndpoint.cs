using Basket.API.Features.ShoppingBasket.Save;

namespace Basket.API.Endpoints.Save;

public class SaveCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts", async (SaveCartRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = request.ToCommand();
                var result = await sender.Send(command, cancellationToken);
                return Results.Created($"/carts/{result.AccountName}", result);
            })
            .WithName("SaveCart")
            .Produces<SaveCartResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Сохраняет корзину для пользователя.")
            .WithDescription("Сохраняет корзину пользователя и возвращает имя аккаунта.");
    }
}