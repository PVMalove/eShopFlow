using catalog.Domain.Entities;

namespace catalog.Domain.Repositories;

public interface IBrandRepository
{
    Task<Brand?> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken);
}