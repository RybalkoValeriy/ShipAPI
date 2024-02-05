using Application.Abstractions.Exceptions;
using Application.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Concrete.Queries;

public class QueryResolver : IQueryResolver
{
    private readonly IServiceProvider _serviceProvider;

    public QueryResolver(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public IQueryHandler<TQuery, TResult> ResolveFor<TQuery, TResult>() where TQuery : BaseQuery =>
        _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>() ?? throw new UnresolvedDependencyException("Can't resolve query handler");
}
