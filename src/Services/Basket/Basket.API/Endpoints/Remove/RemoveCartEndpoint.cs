using Basket.API.Features.ShoppingBasket.Save;

namespace Basket.API.Endpoints.Remove;

public class RemoveCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts/{accountName}", async (RemoveCartRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = request.ToCommand();
                var result = await sender.Send(command, cancellationToken);
                return Results.Ok(result);
            })
            .WithName("RemoveCart")
            .Produces<SaveCartResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Удаляет корзину для пользователя по account name.")
            .WithDescription("Удаляет корзину пользователя по account name и возвращает результат действия.");
    }
}