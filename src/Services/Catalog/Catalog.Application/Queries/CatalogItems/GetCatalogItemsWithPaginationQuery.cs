using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Queries.CatalogItems;

public record GetCatalogItemsWithPaginationQuery(int PageIndex, int PageSize, string? SortBy, bool SortDescending) : IRequest<GetCatalogItemsWithPaginationResult>;