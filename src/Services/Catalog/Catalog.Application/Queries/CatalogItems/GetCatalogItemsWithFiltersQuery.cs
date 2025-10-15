using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Queries.CatalogItems;

public record GetCatalogItemsWithFiltersQuery(
    int PageIndex,
    int PageSize,
    string? SortBy = "title",
    bool SortDescending = false,
    string? SearchTerm = null,
    Guid? BrandId = null,
    Guid? CategoryId = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null
) : IRequest<GetCatalogItemsWithPaginationResult>;