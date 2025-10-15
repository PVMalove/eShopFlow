using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Queries.CatalogItems;

public record GetCatalogItemsByBrandTitleQuery(string BrandTitle) : IRequest<GetCatalogItemsByBrandTitleResult>;