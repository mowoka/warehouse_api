public record PageRequest(int page = 1, int pageSize = 10)
{
    public int Skip => (page - 1) * pageSize;
    public int Take => pageSize;
}