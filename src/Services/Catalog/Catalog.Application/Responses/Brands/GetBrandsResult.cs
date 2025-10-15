using catalog.Domain.Entities;

namespace catalog.Application.Responses.Brands;

public record GetBrandsResult(IEnumerable<Brand> Brands);