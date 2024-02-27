using Application.Abstractions.Queries;
using Application.Ships;
using Application.Ships.Get;
using Moq;

namespace Application.UnitTests;

public class QueryHandlersTests
{
    [Test]
    public async Task GetShipByIdQuery_Should_Handler()
    {
        // Arrange
        var query = new GetShipByIdQuery("code");
        var mockCommandHandler = new Mock<IQueryHandler<GetShipByIdQuery, ShipResponse>>();
        var sut = mockCommandHandler.Object;

        // Act
        await sut.SendAsync(query, CancellationToken.None);

        // Assert
        mockCommandHandler.Verify(h => h.SendAsync(query, CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task GetAllShipsQuery_Should_Handler()
    {
        // Arrange
        var query = new GetAllShipsQuery();
        var mockCommandHandler = new Mock<IQueryHandler<GetAllShipsQuery, IEnumerable<ShipResponse>>>();
        var sut = mockCommandHandler.Object;

        // Act
        await sut.SendAsync(query, CancellationToken.None);

        // Assert
        mockCommandHandler.Verify(h => h.SendAsync(query, CancellationToken.None), Times.Once);
    }
}
