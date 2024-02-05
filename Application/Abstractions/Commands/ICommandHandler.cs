namespace Application.Abstractions.Commands;

public interface ICommandHandler<in ICommand> where ICommand : BaseCommand
{
    Task SendAsync(ICommand command, CancellationToken cancellationToken = default);
}

