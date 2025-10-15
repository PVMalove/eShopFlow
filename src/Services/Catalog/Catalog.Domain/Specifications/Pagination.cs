using catalog.Domain.Entities;

namespace catalog.Domain.Specifications;

public record Pagination<T>(
    int PageIndex,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage,
    IReadOnlyList<T> Items
) where T : BaseEntity
{
    public static Pagination<T> Create(int pageIndex, int pageSize, int totalCount, IReadOnlyList<T> items)
    {
        var totalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

        return new Pagination<T>(
            pageIndex,
            pageSize,
            totalCount,
            totalPages,
            pageIndex < totalPages,
            pageIndex > 1,
            items);
    }
}