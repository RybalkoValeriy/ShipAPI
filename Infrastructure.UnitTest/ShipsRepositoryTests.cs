using Domain.Entities.Ships;
using Infrastructure.DataAccess;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests;

public class ShipsRepositoryTests
{
    private DbContextOptions<ShipDbContext>? options;

    [OneTimeSetUp]
    public void InitTestDbContext() =>
        options = new DbContextOptionsBuilder<ShipDbContext>()
            .UseInMemoryDatabase(databaseName: "shipTestDb")
            .Options;

    [Test]
    public async Task CreateShip_Success()
    {
        // Arrange
        var code = TestHelper.GetRandomCode();
        var shipName = "Name";
        var shipLength = 1;
        var shipWidth = 2;
        var ship = Ship.Create(code, shipName, shipLength, shipWidth);

        using var dbContext = new ShipDbContext(options);
        var shipRepository = new ShipsRepository(dbContext);


        // Act
        await shipRepository.CreateAsync(ship);

        // Assert
        var createdShip = await dbContext.Ships.FindAsync(ship.Code);
        Assert.IsNotNull(createdShip);
        Assert.That(actual: ship.Code, Is.EqualTo(expected: createdShip.Code));
        Assert.That(actual: ship.Name, Is.EqualTo(expected: createdShip.Name));
        Assert.That(actual: ship.Length, Is.EqualTo(expected: createdShip.Length));
        Assert.That(actual: ship.Width, Is.EqualTo(expected: createdShip.Width));
    }

    [Test]
    public async Task DeleteShip_Success()
    {
        // Arrange
        var code = TestHelper.GetRandomCode();
        var shipName = "Name";
        var shipLength = 1;
        var shipWidth = 2;
        var ship = Ship.Create(code, shipName, shipLength, shipWidth);

        using var dbContext = new ShipDbContext(options);
        var shipRepository = new ShipsRepository(dbContext);
        await shipRepository.CreateAsync(ship);

        // Act
        await shipRepository.DeleteAsync(ship);

        // Assert
        var shipShouldBeNull = await dbContext.Ships.FindAsync(ship.Code);
        Assert.IsNull(shipShouldBeNull);
    }

    [Test]
    public async Task GetAllShips_Success()
    {
        // Arrange
        var code = TestHelper.GetRandomCode();
        var shipName = "Name";
        var shipLength = 1;
        var shipWidth = 2;
        var ship = Ship.Create(code, shipName, shipLength, shipWidth);

        using var dbContext = new ShipDbContext(options);
        var shipRepository = new ShipsRepository(dbContext);
        await shipRepository.CreateAsync(ship);

        // Act
        var ships = await shipRepository.GetAllAsync();

        // Assert
        var allShipsInDatabase = await dbContext.Ships.ToListAsync();

        Assert.IsTrue(ships.Count == allShipsInDatabase.Count);
    }

    [Test]
    public async Task GetShipById_Success()
    {
        // Arrange
        var code = TestHelper.GetRandomCode();
        var shipName = "Name";
        var shipLength = 1;
        var shipWidth = 2;
        var ship = Ship.Create(code, shipName, shipLength, shipWidth);

        using var dbContext = new ShipDbContext(options);
        var shipRepository = new ShipsRepository(dbContext);
        await shipRepository.CreateAsync(ship);

        // Act
        var shipFromRepository = await shipRepository.GetByIdAsync(code);

        // Assert
        var shipFromDatabase = await dbContext.Ships.FindAsync(ship.Code);

        Assert.IsNotNull(shipFromRepository);
        Assert.IsNotNull(shipFromDatabase);
        Assert.That(actual: shipFromRepository.Code, Is.EqualTo(expected: shipFromDatabase.Code));
        Assert.That(actual: shipFromRepository.Name, Is.EqualTo(expected: shipFromDatabase.Name));
        Assert.That(actual: shipFromRepository.Length, Is.EqualTo(expected: shipFromDatabase.Length));
        Assert.That(actual: shipFromRepository.Width, Is.EqualTo(expected: shipFromDatabase.Width));
    }

    [Test]
    public async Task UpdateShip_Success()
    {
        // Arrange
        var code = TestHelper.GetRandomCode();
        var shipName = "Name";
        var shipLength = 1;
        var shipWidth = 2;
        var ship = Ship.Create(code, shipName, shipLength, shipWidth);

        using var dbContext = new ShipDbContext(options);
        var shipRepository = new ShipsRepository(dbContext);
        await shipRepository.CreateAsync(ship);

        var newShipName = "newName";
        var newShipLength = 100;
        var newShipWidth = 200;
        ship.Update(newShipName, newShipLength, newShipWidth);

        // Act
        await shipRepository.UpdateAsync(ship);

        // Assert
        var shipAfterUpdate = await dbContext.Ships.FindAsync(ship.Code);

        Assert.IsNotNull(shipAfterUpdate);
        Assert.That(actual: ship.Code, Is.EqualTo(expected: shipAfterUpdate.Code));
        Assert.That(actual: ship.Name, Is.EqualTo(expected: shipAfterUpdate.Name));
        Assert.That(actual: ship.Length, Is.EqualTo(expected: shipAfterUpdate.Length));
        Assert.That(actual: ship.Width, Is.EqualTo(expected: shipAfterUpdate.Width));
    }
}

