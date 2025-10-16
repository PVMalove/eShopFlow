using catalog.Application.Queries.CatalogItems;
using catalog.Application.Responses.CatalogItems;
using catalog.Domain.Specifications;

namespace catalog.Application.Handlers.CatalogItems;

public class GetCatalogItemsWithPaginationQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IQueryHandler<GetCatalogItemsWithPaginationQuery, GetCatalogItemsWithPaginationResult>
{
    public async Task<GetCatalogItemsWithPaginationResult> Handle(GetCatalogItemsWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var parameters = new PaginationParameters(
            query.PageIndex,
            query.PageSize,
            query.SortBy,
            query.SortDescending);

        var pagination = await catalogItemRepository.GetCatalogItemsWithPaginationAsync(
            parameters,
            cancellationToken);
        
        var result = new GetCatalogItemsWithPaginationResult(pagination);
        
        return result;
    }
}