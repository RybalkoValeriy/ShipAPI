namespace Application.Abstractions.Exceptions;

public class ShipNotFoundException : Exception
{
    public ShipNotFoundException()
    {
    }

    public ShipNotFoundException(string message) : base(message)
    {
    }

    public ShipNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
