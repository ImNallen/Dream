namespace Domain.Abstractions;

/// <summary>
/// Represents an error with a code and description.
/// Used with the Result pattern for explicit error handling.
/// </summary>
/// <param name="Code">A unique error code for programmatic handling.</param>
/// <param name="Description">A human-readable description of the error.</param>
public sealed record Error(string Code, string Description)
{
    /// <summary>
    /// Represents no error (used for successful results).
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);

    /// <summary>
    /// Represents a null value error.
    /// </summary>
    public static readonly Error NullValue = new("Error.NullValue", "A null value was provided.");

    /// <summary>
    /// Represents a not found error.
    /// </summary>
    public static Error NotFound(string entityName, object id) =>
        new($"{entityName}.NotFound", $"{entityName} with id '{id}' was not found.");

    /// <summary>
    /// Represents a validation error.
    /// </summary>
    public static Error Validation(string code, string description) =>
        new(code, description);

    /// <summary>
    /// Represents a conflict error (e.g., duplicate entry).
    /// </summary>
    public static Error Conflict(string code, string description) =>
        new(code, description);
}

