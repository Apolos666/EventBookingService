namespace Booking.Infrastructure.Data;

public class ApplicationWriteDbConnection(IApplicationDbContext context) 
    : IApplicationWriteDbConnection
{
    private readonly IApplicationDbContext _context = context;

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        return (await _context.Connection.QueryAsync<T>(sql, param, transaction)).AsList();
    }

    public async Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TAssociative, TReturn>(string sql, Func<TFirst, TAssociative, TReturn> map, object param, string SplitOn,
        IDbTransaction transaction = null, CancellationToken cancellationToken = default)
    {
        return (await _context.Connection.QueryAsync<TFirst, TAssociative, TReturn>(sql, map, param, transaction, splitOn: SplitOn)).AsList();
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        return await _context.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        return await _context.Connection.QuerySingleAsync<T>(sql, param, transaction);
    }

    public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        return await _context.Connection.ExecuteAsync(sql, param, transaction);
    }
}