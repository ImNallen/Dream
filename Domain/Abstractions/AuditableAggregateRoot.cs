namespace Domain.Abstractions;

/// <summary>
/// Base class for aggregate roots that require audit trail support.
/// Automatically tracks creation and modification metadata.
/// </summary>
/// <typeparam name="TId">The type of the aggregate's identifier.</typeparam>
public abstract class AuditableAggregateRoot<TId> : AggregateRoot<TId>, IAuditable
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

    protected AuditableAggregateRoot()
    {
    }

    protected AuditableAggregateRoot(TId id) : base(id)
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

