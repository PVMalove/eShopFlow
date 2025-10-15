using catalog.Application.Queries.Brands;
using catalog.Application.Responses.Brands;

namespace catalog.Application.Handlers.Brands;

public class GetBrandsQueryHandlers(IBrandRepository brandRepository) : IRequestHandler<GetBrandsQuery, GetBrandsResult>
{
    public async Task<GetBrandsResult> Handle(GetBrandsQuery query, CancellationToken cancellationToken)
    {
        var brandList = await brandRepository.GetAllBrandsAsync(cancellationToken);
        var result = new GetBrandsResult(brandList);
        return result;
    }
}