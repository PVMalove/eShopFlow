using catalog.Domain.Entities;

namespace catalog.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
}
