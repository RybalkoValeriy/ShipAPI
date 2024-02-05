using Domain.Entities.Ships;
using FluentAssertions;

public class ShipTests
{
    public static string correctCode = "AAAA-1234-A1";

    [TestCase("someShipName", 1, 2)]
    [TestCase("someShipName1", 100, 6000)]
    [TestCase("someShipName2", int.MaxValue, int.MaxValue)]
    [Parallelizable(ParallelScope.All)]
    public void CreateShip_ShouldCorrectly(string name, int length, int width)
    {
        // Arrange + Act
        var ship = Ship.Create(correctCode, name, length, width);

        // Assert
        ship.Should().NotBeNull();
        ship.Name.Should().Be(name);
        ship.Length.Should().Be(length);
        ship.Width.Should().Be(width);
    }

    [TestCase("", 1, 2)]
    [TestCase(" ", 2, 1)]
    [Parallelizable(ParallelScope.All)]
    public void CreateShip_ShouldThrowException_WhenNameIsNullOrEmpty(string name, int length, int width)
    {
        // Arrange + Act + Assert
        FluentActions
            .Invoking(() => Ship.Create(correctCode, name, length, width))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Incorrect Name");
    }

    [TestCase("someShipName", 0, 1)]
    [TestCase("someShipName", -1, 2)]
    [Parallelizable(ParallelScope.All)]
    public void CreateShip_ShouldThrowException_WhenLengthLessOrEqualZero(string name, int length, int width)
    {
        // Arrange + Act + Assert
        FluentActions
            .Invoking(() => Ship.Create(correctCode, name, length, width))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Length should be more than 0");
    }

    [TestCase("someShipName", 1, 0)]
    [TestCase("someShipName", 2, -1)]
    [Parallelizable(ParallelScope.All)]
    public void CreateShip_ShouldThrowException_WhenWidthLessOrEqualZero(string name, int length, int width)
    {
        // Arrange + Act + Assert
        FluentActions
            .Invoking(() => Ship.Create(correctCode, name, length, width))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Width should be more than 0");
    }

    [Test]
    public void UpdateShip_ShouldCorrectly()
    {
        // Arrange
        var shipName = "Name";
        var shipLength = 1;
        var shipWidth = 2;
        var ship = Ship.Create(correctCode, shipName, shipLength, shipWidth);

        var shipNameExpectedAfterUpdate = "updatedShipName";
        var shipLengthExpectedAfterUpdate = 101;
        var shipWidthExpectedAfterUpdate = 102;

        // Act
        ship.Update(shipNameExpectedAfterUpdate, shipLengthExpectedAfterUpdate, shipWidthExpectedAfterUpdate);

        // Assert
        ship.Should().NotBeNull();
        ship.Name.Should().Be(shipNameExpectedAfterUpdate);
        ship.Length.Should().Be(shipLengthExpectedAfterUpdate);
        ship.Width.Should().Be(shipWidthExpectedAfterUpdate);
        ship.Code.Value.Should().Be(correctCode);
    }

    [TestCase("", 1, 2)]
    [TestCase(" ", 2, 1)]
    [Parallelizable(ParallelScope.All)]
    public void UpdateShip_ShouldThrowException_WhenNameIsNullOrEmpty(string name, int length, int width)
    {
        // Arrange
        var shipName = "Name";
        var shipLength = 1;
        var shipWidth = 2;
        var ship = Ship.Create(correctCode, shipName, shipLength, shipWidth);

        // Act + Assert
        FluentActions
            .Invoking(() => ship.Update(name, length, width))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Incorrect Name");
    }

    [TestCase("someShipName", 0, 1)]
    [TestCase("someShipName", -1, 2)]
    [Parallelizable(ParallelScope.All)]
    public void UpdateShip_ShouldThrowException_WhenLengthLessOrEqualZero(string name, int length, int width)
    {
        // Arrange
        var shipName = "Name";
        var shipLength = 1;
        var shipWidth = 2;
        var ship = Ship.Create(correctCode, shipName, shipLength, shipWidth);

        // Act + Assert
        FluentActions
            .Invoking(() => ship.Update(name, length, width))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Length should be more than 0");
    }

    [TestCase("someShipName", 1, 0)]
    [TestCase("someShipName", 2, -1)]
    [Parallelizable(ParallelScope.All)]
    public void UpdateShip_ShouldThrowException_WhenWidthLessOrEqualZero(string name, int length, int width)
    {
        // Arrange
        var shipName = "Name";
        var shipLength = 1;
        var shipWidth = 2;
        var ship = Ship.Create(correctCode, shipName, shipLength, shipWidth);

        // Act + Assert
        FluentActions
            .Invoking(() => ship.Update(name, length, width))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Width should be more than 0");
    }
}
