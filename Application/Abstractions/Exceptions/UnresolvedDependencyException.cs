namespace Application.Abstractions.Exceptions;

public class UnresolvedDependencyException : Exception
{
    public UnresolvedDependencyException()
    {
    }

    public UnresolvedDependencyException(string message) : base(message)
    {
    }

    public UnresolvedDependencyException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
