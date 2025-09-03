namespace ProjectManagement.Application.Abstractions.Messaging;

/// <summary>
/// Marker interface for application events.
/// </summary>
public interface IEvent
{
    DateTime OccurredOn { get; }
}
