using catalog.Application.Commands.CatalogItems;

namespace catalog.API.Controllers.Requests;

public sealed record UpdateCatalogItemRequest(
    string? Title,
    string? ShortDescription,
    string? FullDescription,
    string? ImageUrl,
    Guid? BrandId,
    Guid? CategoryId,
    decimal Price
)
{
    public UpdateCatalogItemCommand ToCommand(Guid Id) =>
        new(
            Id,
            Title,
            ShortDescription,
            FullDescription,
            ImageUrl,
            BrandId, 
            CategoryId,
            Price
        );
}