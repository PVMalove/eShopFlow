using catalog.Application.Commands.CatalogItems;
using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Handlers.CatalogItems;

public class UpdateCatalogItemCommandHandler(
    ICatalogItemRepository catalogItemRepository,
    IBrandRepository brandRepository,
    ICategoryRepository categoryRepository)
    : ICommandHandler<UpdateCatalogItemCommand, UpdateCatalogItemResult>
{
    public async Task<UpdateCatalogItemResult> Handle(UpdateCatalogItemCommand command,
        CancellationToken cancellationToken)
    {
        var existingItem = await catalogItemRepository.GetCategoryItemByIdAsync(command.Id, cancellationToken);

        if (existingItem is null)
        {
            return new UpdateCatalogItemResult(false);
        }
        var originalBrand = existingItem.Brand;
        var originalCategory = existingItem.Category;
        
        var catalogItem = command.Adapt<CatalogItem>();
        
        if (command.BrandId.HasValue)
        {
            var brand = await brandRepository.GetBrandByIdAsync(command.BrandId.Value, cancellationToken);
            if (brand != null)
            {
                catalogItem.Brand = brand;
            }
            //TODO Если brand не найден, можно либо бросить исключение, либо оставить текущий
        }
        else
        {
            catalogItem.Brand = originalBrand;
        }
        
        if (command.CategoryId.HasValue)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(command.CategoryId.Value, cancellationToken);
            if (category != null)
            {
                catalogItem.Category = category;
            }
            //TODO Если category не найден, можно либо бросить исключение, либо оставить текущий
        }
        else
        {
            catalogItem.Category = originalCategory;
        }

        await catalogItemRepository.UpdateCatalogItemAsync(catalogItem, cancellationToken);
        return new UpdateCatalogItemResult(true);
    }
}