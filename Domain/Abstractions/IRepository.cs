namespace Domain.Abstractions;

/// <summary>
/// Generic repository interface for aggregate roots.
/// Specific repositories should extend this with domain-specific query methods.
/// </summary>
/// <typeparam name="TEntity">The aggregate root type.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
public interface IRepository<TEntity, in TId>
    where TEntity : AggregateRoot<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    void Update(TEntity entity);

    /// <summary>
    /// Removes an entity from the repository.
    /// </summary>
    void Remove(TEntity entity);
}

