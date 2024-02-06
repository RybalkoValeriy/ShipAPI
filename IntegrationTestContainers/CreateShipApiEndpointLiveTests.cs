using Application.Ships.Create;
using Xunit;

namespace Integration.Tests;

public class CreateShipApiEndpointLiveTests : IClassFixture<ShipApiApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public CreateShipApiEndpointLiveTests(ShipApiApplicationFactory shipApiApplicationFactory) =>
        _httpClient = shipApiApplicationFactory.CreateClient();

    [Fact]
    public async Task CreateShip_Then_OkIsReturned()
    {
        // Arrange
        var request = new CreateShipCommand("XXXX-1234-X1", "Name", 1, 1);

        // Act
        var response = await _httpClient.PostAsJsonAsync("api/v1/ships", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateShip_WhenIncorrectCode_BadRequest()
    {
        // Arrange
        var request = new CreateShipCommand("incorrectShipCode", "Name", 1, 1);

        // Act
        var response = await _httpClient.PostAsJsonAsync("api/v1/ships", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    // there will be more cases here
}
