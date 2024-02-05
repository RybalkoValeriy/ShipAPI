using Moq;
using Application.Ships.Create;
using Application.Abstractions.Commands;
using Application.Ships.Delete;
using Application.Ships.Update;

namespace Application.UnitTests;

public class CommandHandlersTests
{
    [Test]
    public async Task CreateShipCommand_Should_Handler()
    {
        // Arrange 
        var command = new CreateShipCommand("code", "name", 1, 1);
        var mockCommandHandler = new Mock<ICommandHandler<CreateShipCommand>>();
        var sut = mockCommandHandler.Object;

        // Act
        await sut.SendAsync(command, CancellationToken.None);

        // Assert
        mockCommandHandler.Verify(h => h.SendAsync(command, CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task DeleteShipCommand_Should_Handler()
    {
        // Arrange 
        var command = new DeleteShipCommand("code");
        var mockCommandHandler = new Mock<ICommandHandler<DeleteShipCommand>>();
        var sut = mockCommandHandler.Object;

        // Act
        await sut.SendAsync(command, CancellationToken.None);

        // Assert
        mockCommandHandler.Verify(h => h.SendAsync(command, CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task UpdateShipCommand_Should_Handler()
    {
        // Arrange 
        var command = new UpdateShipCommand("code", "name", 1, 1);
        var mockCommandHandler = new Mock<ICommandHandler<UpdateShipCommand>>();
        var sut = mockCommandHandler.Object;

        // Act
        await sut.SendAsync(command, CancellationToken.None);

        // Assert
        mockCommandHandler.Verify(h => h.SendAsync(command, CancellationToken.None), Times.Once);
    }
}

