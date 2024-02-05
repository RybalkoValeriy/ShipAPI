using Application.Abstractions.Commands;
using Domain.Entities.Ships;
using Infrastructure.Repositories;

namespace Application.Ships.Create;

public class CreateShipCommandHandler : ICommandHandler<CreateShipCommand>
{
    public readonly IShipsRepository _shipsRepository;

    public CreateShipCommandHandler(IShipsRepository shipRepository) =>
        _shipsRepository = shipRepository;

    public async Task SendAsync(CreateShipCommand command, CancellationToken cancellationToken = default)
    {
        var ship = Ship.Create(command.Code, command.Name, command.Length, command.Width);

        await _shipsRepository.CreateAsync(ship, cancellationToken);
    }
}