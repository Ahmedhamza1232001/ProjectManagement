// IProjectService.cs
using ProjectManagement.Application.DTOs.Projects;

namespace ProjectManagement.Application.Abstractions.Services;

public interface IProjectService
{
    Task<ProjectDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ProjectDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProjectDto> CreateAsync(CreateProjectDto dto,CancellationToken cancellationToken);
    Task<ProjectDto> UpdateAsync(UpdateProjectDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
