namespace Domain.Abstractions;

/// <summary>
/// Exception thrown when a domain rule is violated.
/// Use sparingly - prefer the Result pattern for expected failures.
/// Reserve exceptions for truly exceptional/unexpected situations.
/// </summary>
public sealed class DomainException : Exception
{
    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

