using catalog.Domain.Entities;

namespace catalog.Application.Responses.Categories;

public record GetCategoriesResult(IEnumerable<Category> Categories);