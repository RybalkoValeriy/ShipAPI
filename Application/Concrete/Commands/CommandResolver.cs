using Application.Abstractions.Commands;
using Application.Abstractions.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Concrete.Commands;

internal class CommandResolver : ICommandResolver
{
    private readonly IServiceProvider _serviceProvider;

    public CommandResolver(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public ICommandHandler<ICommand> ResolveFor<ICommand>() where ICommand : BaseCommand =>
        _serviceProvider.GetService<ICommandHandler<ICommand>>() ?? throw new UnresolvedDependencyException("Can't resolve command handler");
}
