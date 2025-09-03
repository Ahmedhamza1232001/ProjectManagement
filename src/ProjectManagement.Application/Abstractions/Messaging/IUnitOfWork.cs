namespace ProjectManagement.Application.Abstractions.Messaging;

/// <summary>
/// Encapsulates transaction management.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
