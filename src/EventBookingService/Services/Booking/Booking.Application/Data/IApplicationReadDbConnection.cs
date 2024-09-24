namespace Booking.Application.Data;

public interface IApplicationReadDbConnection
{
    Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TAssociative, TReturn>(string sql, Func<TFirst, TAssociative, TReturn> map, object param, string SplitOn, IDbTransaction transaction = null ,CancellationToken cancellationToken = default);
    Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
}