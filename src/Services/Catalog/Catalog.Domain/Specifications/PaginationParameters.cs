namespace catalog.Domain.Specifications;

public record PaginationParameters(
    int PageIndex,
    int PageSize,
    string? SortBy = "title",
    bool SortDescending = false
)
{
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;

    public PaginationParameters() : this(1, DefaultPageSize)
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