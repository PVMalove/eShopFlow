using Basket.API.Features.ShoppingBasket.Retrieve;

namespace Basket.API.Endpoints.Retrieve;

public class RetrieveCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/carts/{AccountName}", async ([AsParameters] RetrieveCartRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = request.ToQuery();
                var result = await sender.Send(command, cancellationToken);
                return Results.Ok(result);
            })
            .WithName("RetrieveCart")
            .Produces<RetrieveCartResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Получения корзину для пользователя.")
            .WithDescription("Возвращает корзину пользователя по имени аккаунта.");
    }
}