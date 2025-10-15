namespace catalog.Domain.Specifications;

public record FilterParameters(
    int PageIndex,
    int PageSize,
    string? SortBy = "title",
    bool SortDescending = false,
    string? SearchTerm = null,
    Guid? BrandId = null,
    Guid? CategoryId = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null
)
{
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;

    public FilterParameters() : this(1, DefaultPageSize)
    { }

    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public int PageIndex
    {
        get => _pageIndex;
        init => _pageIndex = value < 1 ? 1 : value;
    }

    private readonly int _pageSize = PageSize;
    private readonly int _pageIndex = PageIndex;
}