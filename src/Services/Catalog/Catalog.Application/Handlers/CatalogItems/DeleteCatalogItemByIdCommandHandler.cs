using catalog.Application.Commands.CatalogItems;
using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Handlers.CatalogItems;

public class DeleteCatalogItemByIdCommandHandler(ICatalogItemRepository catalogItemRepository)
    : ICommandHandler<DeleteCatalogItemByIdCommand, DeleteCatalogItemByIdResult>
{
    public async Task<DeleteCatalogItemByIdResult> Handle(DeleteCatalogItemByIdCommand request,
        CancellationToken cancellationToken)
    {
        var catalogItem = await catalogItemRepository.GetCategoryItemByIdAsync(request.Id, cancellationToken);
        if (catalogItem is null)
        {
            return new DeleteCatalogItemByIdResult(false);
        }

        await catalogItemRepository.DeleteCatalogItemAsync(catalogItem.Id, cancellationToken);
        return new DeleteCatalogItemByIdResult(true);
    }
}