using catalog.Domain.Entities;

namespace catalog.Domain.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<Brand>> GetAllBrandsAsync();
}
