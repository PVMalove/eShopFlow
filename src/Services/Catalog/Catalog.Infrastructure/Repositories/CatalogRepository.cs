using catalog.Domain.Specifications;

namespace catalog.Infrastructure.Repositories;

public class CatalogRepository(IDocumentSession session) : IBrandRepository, ICategoryRepository, ICatalogItemRepository
{
    //BrandRepository
    public async Task<Brand?> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await session.LoadAsync<Brand>(id, cancellationToken);
    }

    public async Task<IEnumerable<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken)
    {
        return await session.Query<Brand>().ToListAsync(cancellationToken);
    }

    //CategoryRepository
    public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await session.LoadAsync<Category>(id, cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        return await session.Query<Category>().ToListAsync(cancellationToken);
    }

    //CatalogItemRepository
    public async Task<IEnumerable<CatalogItem>> GetAllCatalogItemsAsync(CancellationToken cancellationToken)
    {
        return await session.Query<CatalogItem>().ToListAsync(cancellationToken);
    }

    public async Task<CatalogItem?> GetCategoryItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await session.LoadAsync<CatalogItem>(id, cancellationToken);
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrandAsync(string brandTitle,
        CancellationToken cancellationToken)
    {
        return await session.Query<CatalogItem>()
            .Where(ci => ci.Brand != null
                         && !string.IsNullOrEmpty(ci.Brand.Title)
                         && ci.Brand.Title.Contains(brandTitle, StringComparison.OrdinalIgnoreCase)
            ).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByTitleAsync(string title,
        CancellationToken cancellationToken)
    {
        return await session.Query<CatalogItem>()
            .Where(ci => !string.IsNullOrEmpty(ci.Title)
                         && ci.Title.Contains(title, StringComparison.OrdinalIgnoreCase)
            ).ToListAsync(cancellationToken);
    }

    public async Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken)
    {
        session.Store(item);
        await session.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<bool> UpdateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken)
    {
        session.Store(item);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteCatalogItemAsync(Guid id, CancellationToken cancellationToken)
    {
        session.Delete<CatalogItem>(id);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Pagination<CatalogItem>> GetCatalogItemsWithPaginationAsync(
        PaginationParameters parameters, CancellationToken cancellationToken)
    {
        var skip = (parameters.PageIndex - 1) * parameters.PageSize;

        var query = session.Query<CatalogItem>().AsQueryable();

        var totalCount = await query.CountAsync(cancellationToken);

        var sortedQuery = parameters.SortBy?.ToLower() switch
        {
            "title" => parameters.SortDescending
                ? query.OrderByDescending(x => x.Title)
                : query.OrderBy(x => x.Title),
            "price" => parameters.SortDescending
                ? query.OrderByDescending(x => x.Price)
                : query.OrderBy(x => x.Price),
            _ => query.OrderBy(x => x.Title)
        };

        var items = await sortedQuery
            .Skip(skip)
            .Take(parameters.PageSize)
            .ToListAsync(cancellationToken);

        return Pagination<CatalogItem>.Create(
            parameters.PageIndex,
            parameters.PageSize,
            totalCount,
            items);
    }

    public async Task<Pagination<CatalogItem>> GetCatalogItemsWithFiltersAsync(
        FilterParameters parameters, CancellationToken cancellationToken)
    {
        var skip = (parameters.PageIndex - 1) * parameters.PageSize;

        var query = session.Query<CatalogItem>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
        {
            query = query.Where(x =>
                (x.Title != null && x.Title.ToLower().Contains(parameters.SearchTerm.ToLower())) ||
                (x.ShortDescription != null &&
                 x.ShortDescription.Contains(parameters.SearchTerm, StringComparison.CurrentCultureIgnoreCase)) ||
                (x.FullDescription != null && x.FullDescription.ToLower().Contains(parameters.SearchTerm.ToLower())));
        }

        if (parameters.BrandId.HasValue)
        {
            query = query.Where(x => x.Brand != null && x.Brand.Id == parameters.BrandId.Value);
        }

        if (parameters.CategoryId.HasValue)
        {
            query = query.Where(x => x.Category != null && x.Category.Id == parameters.CategoryId.Value);
        }

        if (parameters.MinPrice.HasValue)
        {
            query = query.Where(x => x.Price >= parameters.MinPrice.Value);
        }

        if (parameters.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Price <= parameters.MaxPrice.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortedQuery = parameters.SortBy?.ToLower() switch
        {
            "title" => parameters.SortDescending
                ? query.OrderByDescending(x => x.Title)
                : query.OrderBy(x => x.Title),
            "price" => parameters.SortDescending
                ? query.OrderByDescending(x => x.Price)
                : query.OrderBy(x => x.Price),
            _ => query.OrderBy(x => x.Title)
        };

        var items = await sortedQuery
            .Skip(skip)
            .Take(parameters.PageSize)
            .ToListAsync(cancellationToken);

        return Pagination<CatalogItem>.Create(
            parameters.PageIndex,
            parameters.PageSize,
            totalCount,
            items);
    }
}