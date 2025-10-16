using catalog.Application.Queries.CatalogItems;
using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Handlers.CatalogItems;

public class GetCatalogItemsByBrandTitleQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IQueryHandler<GetCatalogItemsByBrandTitleQuery, GetCatalogItemsByBrandTitleResult>
{
    public async Task<GetCatalogItemsByBrandTitleResult> Handle(GetCatalogItemsByBrandTitleQuery query,
        CancellationToken cancellationToken)
    {
        var catalogItem = await catalogItemRepository.GetCatalogItemsByBrandAsync(query.BrandTitle, cancellationToken);
        var result = new GetCatalogItemsByBrandTitleResult(catalogItem);
        return result;
    }
}