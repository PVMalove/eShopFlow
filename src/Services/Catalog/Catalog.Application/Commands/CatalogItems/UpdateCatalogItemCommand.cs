using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Commands.CatalogItems;

public record UpdateCatalogItemCommand(
    Guid Id,
    string? Title,
    string? ShortDescription,
    string? FullDescription,
    string? ImageUrl,
    Guid? BrandId,
    Guid? CategoryId,
    decimal Price
) : IRequest<UpdateCatalogItemResult>;