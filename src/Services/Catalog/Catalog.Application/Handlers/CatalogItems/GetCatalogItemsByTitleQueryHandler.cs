using catalog.Application.Queries.CatalogItems;
using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Handlers.CatalogItems;

public class GetCatalogItemsByTitleQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IQueryHandler<GetCatalogItemsByTitleQuery, GetCatalogItemsByTitleResult>
{
    public async Task<GetCatalogItemsByTitleResult> Handle(GetCatalogItemsByTitleQuery query,
        CancellationToken cancellationToken)
    {
        var catalogItem = await catalogItemRepository.GetCatalogItemsByTitleAsync(query.Title, cancellationToken);
        var result = new GetCatalogItemsByTitleResult(catalogItem);
        return result;
    }
}