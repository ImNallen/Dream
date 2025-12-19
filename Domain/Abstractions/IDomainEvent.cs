using MediatR;

namespace Domain.Abstractions;

/// <summary>
/// Marker interface for domain events.
/// Domain events represent something significant that happened in the domain.
/// Implements INotification for MediatR integration.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// The date and time when the event occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}

