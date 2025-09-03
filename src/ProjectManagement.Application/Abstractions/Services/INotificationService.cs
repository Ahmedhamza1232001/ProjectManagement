namespace ProjectManagement.Application.Abstractions.Services;

public interface INotificationService
{
    Task NotifyUsersAsync(IEnumerable<Guid> userIds, string message, CancellationToken cancellationToken = default);
}
