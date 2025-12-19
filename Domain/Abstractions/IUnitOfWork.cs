namespace Domain.Abstractions;

/// <summary>
/// Unit of Work interface for coordinating changes across multiple repositories.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all changes made in this unit of work to the underlying store.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of state entries written to the store.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

