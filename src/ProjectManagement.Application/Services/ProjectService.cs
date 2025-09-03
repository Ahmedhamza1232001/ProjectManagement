using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Application.DTOs.Projects;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetByIdAsync(id, cancellationToken);
        if (project is null) return null;

        return new ProjectDto(project.Id, project.Name, project.Description);
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var projects = await _projectRepository.GetAllAsync(cancellationToken);
        return projects.Select(p => new ProjectDto(p.Id, p.Name, p.Description));
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectDto dto, CancellationToken cancellationToken = default)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description
        };

        await _projectRepository.AddAsync(project, cancellationToken);

        return new ProjectDto(project.Id, project.Name, project.Description);
    }

    public async Task<ProjectDto?> UpdateAsync(UpdateProjectDto dto, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (project is null) return null;

        project.Name = dto.Name;
        project.Description = dto.Description;

        await _projectRepository.UpdateAsync(project, cancellationToken);

        return new ProjectDto(project.Id, project.Name, project.Description);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetByIdAsync(id, cancellationToken);
        if (project is null) return false;

        await _projectRepository.DeleteAsync(project, cancellationToken);
        return true;
    }
}
