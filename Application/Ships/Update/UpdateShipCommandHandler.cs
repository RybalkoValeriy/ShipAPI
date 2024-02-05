using Application.Abstractions.Commands;
using Application.Abstractions.Exceptions;
using Domain.Entities.Ships;
using Infrastructure.Repositories;

namespace Application.Ships.Update;

public class UpdateShipCommandHandler : ICommandHandler<UpdateShipCommand>
{
    public readonly IShipsRepository _shipsRepository;

    public UpdateShipCommandHandler(IShipsRepository shipRepository) =>
        _shipsRepository = shipRepository;

    public async Task SendAsync(UpdateShipCommand command, CancellationToken cancellationToken = default)
    {
        var ship = await _shipsRepository.GetByIdAsync(command.Code, cancellationToken)
            ?? throw new ShipNotFoundException($"Ship with current code:`{command.Code}` is not found");

        ship.Update(
            command.Name,
            command.Width,
            command.Length
            );

        await _shipsRepository.UpdateAsync(ship, cancellationToken);
    }
}
