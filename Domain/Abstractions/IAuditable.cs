namespace Domain.Abstractions;

/// <summary>
/// Interface for entities that track creation and modification metadata.
/// Implement this on entities that need audit trail support.
/// The Infrastructure layer will automatically populate these values.
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// The date and time when the entity was created (UTC).
    /// </summary>
    DateTime CreatedAtUtc { get; }

    /// <summary>
    /// The identifier of the user who created the entity.
    /// </summary>
    string? CreatedBy { get; }

    /// <summary>
    /// The date and time when the entity was last modified (UTC).
    /// </summary>
    DateTime? ModifiedAtUtc { get; }

    /// <summary>
    /// The identifier of the user who last modified the entity.
    /// </summary>
    string? ModifiedBy { get; }

    /// <summary>
    /// Sets the creation audit information.
    /// Called by the Infrastructure layer when the entity is first persisted.
    /// </summary>
    void SetCreated(DateTime createdAtUtc, string? createdBy);

    /// <summary>
    /// Sets the modification audit information.
    /// Called by the Infrastructure layer when the entity is updated.
    /// </summary>
    void SetModified(DateTime modifiedAtUtc, string? modifiedBy);
}

