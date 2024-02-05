using Domain.Entities.Ships;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ShipsRepository : IShipsRepository
{
    public readonly ShipDbContext _dbContext;

    public ShipsRepository(ShipDbContext dbContext) => _dbContext = dbContext;

    public async Task CreateAsync(Ship ship, CancellationToken cancellationToken = default)
    {
        _dbContext.Ships.Add(ship);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Ship ship, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(ship); 
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<Ship>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Ships.ToListAsync(cancellationToken);

    public Task<Ship?> GetByIdAsync(string code, CancellationToken cancellationToken = default) =>
        _dbContext.Ships.FirstOrDefaultAsync(x => x.Code.Value.Equals(code, StringComparison.OrdinalIgnoreCase), cancellationToken);

    public async Task UpdateAsync(Ship ship, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(ship);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}