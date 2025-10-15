using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Queries.CatalogItems;

public record GetCatalogItemsByTitleQuery(string Title) : IRequest<GetCatalogItemsByTitleResult>;