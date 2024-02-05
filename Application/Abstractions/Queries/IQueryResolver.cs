namespace Application.Abstractions.Queries;

public interface IQueryResolver
{
    IQueryHandler<TQuery, TResult> ResolveFor<TQuery, TResult>() where TQuery : BaseQuery;
}


