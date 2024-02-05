using Domain.Entities.Ships;

namespace Infrastructure.Repositories;

public interface IShipsRepository
{
    Task CreateAsync(Ship ship, CancellationToken cancellationToken = default);
    Task UpdateAsync(Ship ship, CancellationToken cancellationToken = default);
    Task DeleteAsync(Ship ship, CancellationToken cancellationToken = default);
    Task<List<Ship>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Ship?> GetByIdAsync(string code, CancellationToken cancellationToken = default);
}
