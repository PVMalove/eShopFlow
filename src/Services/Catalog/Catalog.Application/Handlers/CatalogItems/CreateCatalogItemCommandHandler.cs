using catalog.Application.Commands.CatalogItems;
using catalog.Application.Responses.CatalogItems;

namespace catalog.Application.Handlers.CatalogItems;

public class CreateCatalogItemCommandHandler(
    ICatalogItemRepository catalogItemRepository,
    IBrandRepository brandRepository,
    ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCatalogItemCommand, CreateCatalogItemResult>
{
    public async Task<CreateCatalogItemResult> Handle(CreateCatalogItemCommand command,
        CancellationToken cancellationToken)
    {
        var catalogItem = command.Adapt<CatalogItem>();
        catalogItem.Id = Guid.NewGuid();
        
        if (command.BrandId.HasValue)
        {
            var brand = await brandRepository.GetBrandByIdAsync(command.BrandId.Value, cancellationToken);
            if (brand != null)
            {
                //TODO Можно бросить исключение или вернуть ошибку
            }
            catalogItem.Brand = brand;
        }

        if (command.CategoryId.HasValue)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(command.CategoryId.Value, cancellationToken);
            if (category == null)
            {
                //TODO Можно бросить исключение или вернуть ошибку
            }
            catalogItem.Category = category;
        }
        
        await catalogItemRepository.CreateCatalogItemAsync(catalogItem, cancellationToken);
        return new CreateCatalogItemResult(catalogItem.Id);
    }
}