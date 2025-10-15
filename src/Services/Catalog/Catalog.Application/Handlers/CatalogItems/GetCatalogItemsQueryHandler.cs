using catalog.Application.Queries.CatalogItems;
using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Handlers.CatalogItems;

public class GetCatalogItemsQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IRequestHandler<GetCatalogItemsQuery, GetCatalogItemsResult>
{
    public async Task<GetCatalogItemsResult> Handle(GetCatalogItemsQuery query, CancellationToken cancellationToken)
    {
        var catalogItems = await catalogItemRepository.GetAllCatalogItemsAsync(cancellationToken);
        var result = new GetCatalogItemsResult(catalogItems);
        return result;
    }
}