using catalog.Domain.Specifications;

namespace catalog.Application.Responses.CatalogItems;

public record GetCatalogItemsWithPaginationResult(Pagination<CatalogItem> CatalogItems);