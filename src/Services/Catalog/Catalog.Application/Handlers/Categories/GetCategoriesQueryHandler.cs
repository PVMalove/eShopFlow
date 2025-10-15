using catalog.Application.Queries.Categories;
using catalog.Application.Responses.Categories;

namespace catalog.Application.Handlers.Categories;

public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, GetCategoriesResult>
{
    public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categoryList = await categoryRepository.GetAllCategoriesAsync(cancellationToken);
        var result = new GetCategoriesResult(categoryList);
        return result;
    }
}