namespace Application.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TResult> where TQuery : BaseQuery
{
    Task<TResult> SendAsync(TQuery query, CancellationToken cancellationToken = default);
}