using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Abstractions.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Project project, CancellationToken cancellationToken);
    Task UpdateAsync(Project project, CancellationToken cancellationToken);
    Task DeleteAsync(Project project, CancellationToken cancellationToken);
    Task<List<Project>> SearchAsync(string keyword, CancellationToken cancellationToken);

}
