using Domain.Entities.Ships;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class ShipDbContext : DbContext
{
    public ShipDbContext()
    {
    }

    public ShipDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Ship> Ships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ShipConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
