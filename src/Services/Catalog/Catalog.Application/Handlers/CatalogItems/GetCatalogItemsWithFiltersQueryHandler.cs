using catalog.Application.Queries.CatalogItems;
using catalog.Application.Responses.CatalogItems;
using catalog.Domain.Specifications;

namespace catalog.Application.Handlers.CatalogItems;

public class GetCatalogItemsWithFiltersQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IQueryHandler<GetCatalogItemsWithFiltersQuery, GetCatalogItemsWithPaginationResult>
{
    public async Task<GetCatalogItemsWithPaginationResult> Handle(
        GetCatalogItemsWithFiltersQuery query,
        CancellationToken cancellationToken)
    {
        var parameters = new FilterParameters(
            query.PageIndex,
            query.PageSize,
            query.SortBy,
            query.SortDescending,
            query.SearchTerm,
            query.BrandId,
            query.CategoryId,
            query.MinPrice,
            query.MaxPrice);

        var pagination = await catalogItemRepository.GetCatalogItemsWithFiltersAsync(
            parameters, cancellationToken);

        return new GetCatalogItemsWithPaginationResult(pagination);
    }
}