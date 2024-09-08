namespace BuildingBlocks.Pagination;

public class PaginatedResult<TEntity>
    (int pageIndex, int pageSize, long count, bool hasNextPage, bool hasPreviousPage, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex => pageIndex;
    public int PageSize => pageSize;
    public long Count => count;
    public bool HasNextPage => hasNextPage;
    public bool HasPreviousPage => hasPreviousPage;
    public IEnumerable<TEntity> Data => data;
}