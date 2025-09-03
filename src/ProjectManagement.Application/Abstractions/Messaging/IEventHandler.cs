namespace ProjectManagement.Application.Abstractions.Messaging;

/// <summary>
/// Defines a handler for a specific event type.
/// </summary>
/// <typeparam name="TEvent">The event type.</typeparam>
public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
