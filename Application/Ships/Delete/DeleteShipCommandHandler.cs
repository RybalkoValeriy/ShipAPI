using Application.Abstractions.Commands;
using Application.Abstractions.Exceptions;
using Infrastructure.Repositories;

namespace Application.Ships.Delete;

public class DeleteShipCommandHandler : ICommandHandler<DeleteShipCommand>
{
    public readonly IShipsRepository _shipsRepository;

    public DeleteShipCommandHandler(IShipsRepository shipRepository) =>
        _shipsRepository = shipRepository;

    public async Task SendAsync(DeleteShipCommand command, CancellationToken cancellationToken = default)
    {
        var ship = await _shipsRepository.GetByIdAsync(command.Code, cancellationToken)
            ?? throw new ShipNotFoundException($"Ship with current code:`{command.Code}` is not found");

        await _shipsRepository.DeleteAsync(ship, cancellationToken);
    }
}
