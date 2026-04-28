public record SearchRequest(string Keyword, int Page = 1, int PageSize = 10)
{
    public int Skip => (Page - 1) * PageSize;
    public int Take => PageSize;
}