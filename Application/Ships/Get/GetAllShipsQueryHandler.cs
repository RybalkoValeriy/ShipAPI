using Application.Abstractions.Queries;
using Infrastructure.Repositories;

namespace Application.Ships.Get;

public class GetAllShipsQueryHandler : IQueryHandler<GetAllShipsQuery, IEnumerable<ShipResponse>>
{
    private readonly IShipsRepository _shipsRepository;

    public GetAllShipsQueryHandler(IShipsRepository shipsRepository) => _shipsRepository = shipsRepository;

    public async Task<IEnumerable<ShipResponse>> SendAsync(GetAllShipsQuery query, CancellationToken cancellationToken = default)
    {
        var ships = await _shipsRepository.GetAllAsync(cancellationToken);

        return ships
            .Select(ship => new ShipResponse(ship.Code.Value, ship.Name, ship.Length, ship.Width));
    }
}
