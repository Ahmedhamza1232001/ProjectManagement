using ProjectManagement.Application.Abstractions.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        // Simple in-memory store for notifications (for demo purposes)
        private static readonly ConcurrentDictionary<Guid, List<string>> _notifications = new();

        public Task NotifyUsersAsync(IEnumerable<Guid> userIds, string message, CancellationToken cancellationToken = default)
        {
            foreach (var userId in userIds)
            {
                _notifications.AddOrUpdate(
                    userId,
                    new List<string> { message },
                    (key, existingList) =>
                    {
                        existingList.Add(message);
                        return existingList;
                    });
            }

            // In real application, you might send emails, push notifications, or use SignalR
            return Task.CompletedTask;
        }

        // Optional: method to get notifications (for testing)
        public Task<List<string>> GetUserNotificationsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            _notifications.TryGetValue(userId, out var messages);
            return Task.FromResult(messages ?? new List<string>());
        }
    }
}