using Application.Abstractions.Exceptions;
using Application.Abstractions.Queries;
using Infrastructure.Repositories;

namespace Application.Ships.Get;

public class GetShipByIdQueryHandler : IQueryHandler<GetShipByIdQuery, ShipResponse>
{
    private readonly IShipsRepository _shipsRepository;

    public GetShipByIdQueryHandler(IShipsRepository shipsRepository) => _shipsRepository = shipsRepository;

    public async Task<ShipResponse> SendAsync(GetShipByIdQuery query, CancellationToken cancellationToken = default)
    {
        var ship = await _shipsRepository.GetByIdAsync(query.Code, cancellationToken);

        return ship is null
            ? throw new ShipNotFoundException($"Ship with current code:`{query.Code}` is not found")
            : new ShipResponse(ship.Code.Value, ship.Name, ship.Length, ship.Width);
    }
}