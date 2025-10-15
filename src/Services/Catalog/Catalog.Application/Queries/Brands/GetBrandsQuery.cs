using catalog.Application.Responses.Brands;

namespace catalog.Application.Queries.Brands;

public record GetBrandsQuery : IRequest<GetBrandsResult>;