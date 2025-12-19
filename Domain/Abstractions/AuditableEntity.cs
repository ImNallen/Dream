namespace Domain.Abstractions;

/// <summary>
/// Base class for entities (non-aggregate roots) that require audit trail support.
/// Use this for child entities within an aggregate that need their own audit tracking.
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
public abstract class AuditableEntity<TId> : Entity<TId>, IAuditable
    where TId : notnull
{
    /// <inheritdoc />
    public DateTime CreatedAtUtc { get; private set; }

    /// <inheritdoc />
    public string? CreatedBy { get; private set; }

    /// <inheritdoc />
    public DateTime? ModifiedAtUtc { get; private set; }

    /// <inheritdoc />
    public string? ModifiedBy { get; private set; }

    protected AuditableEntity()
    {
    }

    protected AuditableEntity(TId id) : base(id)
    {
    }

    /// <inheritdoc />
    public void SetCreated(DateTime createdAtUtc, string? createdBy)
    {
        CreatedAtUtc = createdAtUtc;
        CreatedBy = createdBy;
    }

    /// <inheritdoc />
    public void SetModified(DateTime modifiedAtUtc, string? modifiedBy)
    {
        ModifiedAtUtc = modifiedAtUtc;
        ModifiedBy = modifiedBy;
    }
}

