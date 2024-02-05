using System.Text.Json;

namespace ShipApi.Models;

public record ErrorDetails(int StatusCode, string Message)
{
    public override string ToString() => JsonSerializer.Serialize(this);
}
