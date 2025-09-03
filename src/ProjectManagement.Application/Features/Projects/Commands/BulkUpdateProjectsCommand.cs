using MediatR;
using ProjectManagement.Application.DTOs.Projects;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Projects.Commands;

public record BulkUpdateProjectsCommand(List<UpdateProjectDto> Projects) : IRequest<List<ProjectDto>>;

public class BulkUpdateProjectsCommandHandler : IRequestHandler<BulkUpdateProjectsCommand, List<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;

    public BulkUpdateProjectsCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<ProjectDto>> Handle(BulkUpdateProjectsCommand request, CancellationToken cancellationToken)
    {
        var updated = new List<ProjectDto>();

        foreach (var dto in request.Projects)
        {
            var project = await _projectRepository.GetByIdAsync(dto.Id,cancellationToken);
            if (project is not null)
            {
                project.Name = dto.Name;
                project.Description = dto.Description;

                await _projectRepository.UpdateAsync(project,cancellationToken);

                updated.Add(new ProjectDto(project.Id, project.Name, project.Description));
            }
        }

        return updated;
    }
}
