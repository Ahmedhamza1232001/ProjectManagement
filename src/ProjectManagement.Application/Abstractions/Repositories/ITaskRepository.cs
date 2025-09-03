using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Abstractions.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<TaskItem>> SearchAsync(string keyword, CancellationToken cancellationToken);
    Task AddAsync(TaskItem task, CancellationToken cancellationToken);
    Task UpdateAsync(TaskItem task,CancellationToken cancellationToken);
    Task DeleteAsync(TaskItem task, CancellationToken cancellationToken);
}
