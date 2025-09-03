using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Abstractions.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Project>> GetAllAsync();
    Task AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Project project);
    Task<List<Project>> SearchAsync(string keyword);

}
