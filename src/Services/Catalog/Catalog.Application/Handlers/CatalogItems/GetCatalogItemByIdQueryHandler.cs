using catalog.Application.Queries.CatalogItems;
using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Handlers.CatalogItems;

public class GetCatalogItemByIdQueryHandler(ICatalogItemRepository catalogItemRepository)
    : IQueryHandler<GetCatalogItemByIdQuery, GetCatalogItemByIdResult>
{
    public async Task<GetCatalogItemByIdResult> Handle(GetCatalogItemByIdQuery query,
        CancellationToken cancellationToken)
    {
        var catalogItem = await catalogItemRepository.GetCategoryItemByIdAsync(query.Id, cancellationToken);
        var result = new GetCatalogItemByIdResult(catalogItem);
        return result;
    }
}