namespace Application.Abstractions.Commands;

public interface ICommandResolver
{
    ICommandHandler<ICommand> ResolveFor<ICommand>() where ICommand : BaseCommand;
}
