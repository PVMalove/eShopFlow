using catalog.Domain.Entities;

namespace catalog.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);
}