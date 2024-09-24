namespace Booking.Infrastructure.Data;

public class ApplicationReadDbConnection(IConfiguration configuration) 
    : IApplicationReadDbConnection, IDisposable
{
    private readonly IDbConnection _connection = new MySqlConnection(configuration.GetConnectionString("Database"));

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        return (await _connection.QueryAsync<T>(sql, param, transaction)).AsList();
    }

    public async Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TAssociative, TReturn>(string sql, Func<TFirst, TAssociative, TReturn> map, object param, string SplitOn,
        IDbTransaction transaction = null, CancellationToken cancellationToken = default)
    {
        return (await _connection.QueryAsync<TFirst, TAssociative, TReturn>(sql, map, param, transaction, splitOn: SplitOn)).AsList();
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
        CancellationToken cancellationToken = default)
    {
        return await _connection.QuerySingleAsync<T>(sql, param, transaction);
    }

    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}